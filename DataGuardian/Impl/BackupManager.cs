using DataGuardian.GUI;
using DataGuardian.Plugins;
using DataGuardian.Windows;
using DataGuardian.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGuardian.Impl
{
    public class BackupManager : PluginBase, IBackupManager
    {
        private BackupDbWorker _dbWorker;
        private readonly List<IBackupScript> _scripts = new List<IBackupScript>();
        public event EventHandler<IEnumerable<IBackupScript>> BackupScriptsChanged;
        public IEnumerable<IBackupScript> BackupScripts => _scripts;

        public override void Init(ICore core)
        {
            base.Init(core);

            _dbWorker = new BackupDbWorker(Core.Settings.ConnectionString);
            _scripts.Clear();
            _scripts.AddRange(_dbWorker.Read());

            Task.Run(PerformBackups);
        }

        public async Task Perform(IBackupScript script)
        {
            await PerformScript(script, true);
        }

        private async Task PerformBackups()
        {
            var backupScripts = GetScriptsToPerform();

            foreach (var backupScript in backupScripts)
            {
                await PerformScript(backupScript, false);
            }
        }

        private async Task PerformScript(IBackupScript backupScript, bool force)
        {
            backupScript.CurrentState = BackupCurrentState.Processing;

            foreach (var step in backupScript.Steps)
            {
                if (NeedPerform(step) || force)
                    await step.Perform();
            }

            EditBackupScript(backupScript, backupScript);

            backupScript.CurrentState = BackupCurrentState.Finished;
        }

        private IEnumerable<IBackupScript> GetScriptsToPerform()
        {
            return BackupScripts.Where(x => x.Enabled && x.Steps.Any(NeedPerform)).ToList();
        }

        private static bool NeedPerform(IBackupStep s)
        {
            return s.NextPerformDate.Date == DateTime.Today;
        }

        public void RemoveBackupScript(IBackupScript selectedBackupScript)
        {
            try
            {
                if (selectedBackupScript == null)
                    return;

                _dbWorker.Delete(selectedBackupScript);
                _scripts.Remove(selectedBackupScript);

                Notify();
            }
            catch (Exception ex)
            {
                Core.Logger.Log("RemoveBackupScript", ex);
            }
        }

        public void EditBackupScriptGui(IBackupScript backupScript)
        {
            using (var wnd = new WndCreateEditBackupScript(backupScript, null))
            {
                if (wnd.ShowDialog() == DialogResult.OK && wnd.NewBackupScript != null)
                {
                    var newBackupScript = wnd.NewBackupScript;
                    if (newBackupScript == null)
                        return;

                    EditBackupScript(backupScript, newBackupScript);

                    GuiHelper.ShowMessage("Backup script updated");
                }
            }
        }

        public void EditBackupScript(IBackupScript backupScript, IBackupScript newBackupScript)
        {
            _dbWorker.Edit(newBackupScript);

            _scripts.Remove(backupScript);
            _scripts.Add(newBackupScript);

            Notify();
        }

        private void Notify()
        {
            BackupScriptsChanged?.Invoke(this, BackupScripts);
        }

        public void CreateBackupScriptGui()
        {
            try
            {
                using (var selectPathWnd = new WndEnterPath(
                    "Please enter name and path for your backup script", 
                    $"New_backup_{Guid.NewGuid()}", 
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)))
                {
                    if (selectPathWnd.ShowDialog() != DialogResult.OK)
                        return;

                    using (var wnd = new WndCreateEditBackupScript(null, selectPathWnd.Params))
                    {
                        if (wnd.ShowDialog() == DialogResult.OK)
                        {
                            var newBackupScript = wnd.NewBackupScript;
                            if (newBackupScript == null) 
                                return;

                            AddNewBackupScript(newBackupScript);

                            GuiHelper.ShowMessage("Backup script created");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage("Backup script failed");
                Core.Logger.Log(InfoLogLevel.Error, nameof(CreateBackupScriptGui), "Backup script failed", ex);
            }
        }

        public void AddNewBackupScript(IBackupScript newBackupScript)
        {
            _dbWorker.Save(newBackupScript);
            _scripts.Add(newBackupScript);

            Notify();

            Task.Run(PerformBackups);
        }
    }
}