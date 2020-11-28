using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataGuardian.GUI;
using DataGuardian.Plugins;
using DataGuardian.Windows;
using DataGuardian.Workers;

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
        }

        public void RemoveBackupScript(IBackupScript selectedBackupScript)
        {
        }

        public void EditBackupScript(IBackupScript backupScript)
        {
            using (var wnd = new WndCreateEditBackupScript(backupScript, null))
            {
                wnd.ShowDialog();
            }
        }

        public void RequestNewBackupScriptDialog()
        {
            using (var selectPathWnd = new WndEnterPath())
            {
                if (selectPathWnd.ShowDialog() != DialogResult.OK)
                    return;

                using (var wnd = new WndCreateEditBackupScript(null, selectPathWnd.Params))
                {
                    if (wnd.ShowDialog() == DialogResult.OK && wnd.NewBackupScript != null)
                    {
                        var newBackupScript = wnd.NewBackupScript;

                        _dbWorker.Save(newBackupScript);
                        _scripts.Add(newBackupScript);
                        BackupScriptsChanged?.Invoke(this, BackupScripts);

                        GuiHelper.ShowMessage("Backup script created");
                    }
                }
            }
        }
    }
}