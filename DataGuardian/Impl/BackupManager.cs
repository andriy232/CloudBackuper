using DataGuardian.GUI;
using DataGuardian.Plugins;
using DataGuardian.Windows;
using DataGuardian.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGuardian.Controls;
using DataGuardian.GUI.Windows;
using DataGuardian.Properties;
using Timer = System.Windows.Forms.Timer;

namespace DataGuardian.Impl
{
    public class BackupManager : PluginBase, IBackupManager
    {
        private readonly List<IBackupScript> _scripts = new List<IBackupScript>();
        private object _locker = new object();
        private bool _performingJob = false;
        private BackupDbWorker _dbWorker;
        private Timer _timer;
        private CancellationTokenSource _cancellationTokenSource;

        public event EventHandler<IEnumerable<IBackupScript>> BackupScriptsChanged;

        public IEnumerable<IBackupScript> BackupScripts => _scripts;

        public override void Init(ICore core)
        {
            base.Init(core);

            _dbWorker = new BackupDbWorker(Core.Settings.ConnectionString);
            _scripts.Clear();
            _scripts.AddRange(_dbWorker.Read());

            Core.GuiManager.GuiLoaded += OnGuiLoaded;
        }

        private void OnGuiLoaded(object sender, EventArgs e)
        {
            StartTimer();
        }


        private void StartTimer()
        {
            _timer = new Timer {Interval = (int) TimeSpan.FromMinutes(1).TotalSeconds};
            _timer.Tick += OnTimerTick;
            _timer.Start();

            _cancellationTokenSource = new CancellationTokenSource();
        }

        private async void OnTimerTick(object sender, EventArgs e)
        {
            await StartCronJob(_cancellationTokenSource.Token);
        }

        private async Task StartCronJob(CancellationToken cancellationToken)
        {
            lock (_locker)
                if (_performingJob)
                    return;

            lock (_locker)
                _performingJob = true;

            foreach (var backupScript in GetScriptsToPerform())
            {
                try
                {
                    foreach (var backupStep in backupScript.Steps.ToList())
                    {
                        var needRestore = NeedRestoreStep(backupStep);
                        if (needRestore)
                            await PerformRestore(backupScript, backupStep, cancellationToken);
                    }

                    await Perform(backupScript);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log($"Error during perform Local files check", ex);
                }
            }

            lock (_locker)
                _performingJob = false;
        }

        private static bool NeedRestoreStep(IBackupStep backupStep)
        {
            return backupStep.Action == BackupAction.BackupTo && !backupStep.CheckIfLocalCopyExists();
        }

        private async Task PerformRestore(
            IBackupScript script,
            IBackupStep failed, 
            CancellationToken cancellationToken)
        {
            var dialogResult = GuiHelper.ShowConfirmationDialog(
                $"Seems local version of '{script.Name}' - [{failed.TargetPath}] missing.{Environment.NewLine}Do you want to restore it from last remote backup?");

            if (dialogResult == DialogResult.OK)
            {
                var backupStep = new BackupStep
                {
                    Account = failed.Account,
                    Action = BackupAction.RestoreTo,
                    TargetPath = failed.TargetPath,
                    ActionParameter = failed.TargetPath,
                    Period = BackupPeriod.OneTime,
                    StartDate = DateTime.Now,
                    BackupFileName = failed.BackupFileName
                };

                script.Steps.Add(backupStep);
                EditBackupScript(script, script);

                await PerformScript(script, true, cancellationToken);

                script.Steps.Remove(backupStep);
                EditBackupScript(script, script);

                GuiHelper.ShowMessage(failed.CheckIfLocalCopyExists()
                    ? "Data restored successfully"
                    : "Data not restored, please check");
            }
        }

        public async Task Perform(IBackupScript script)
        {
            await PerformScript(script, true, _cancellationTokenSource.Token);
        }

        private async Task PerformScript(IBackupScript backupScript, bool force, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            backupScript.CurrentState = BackupCurrentState.Processing;

            foreach (var step in backupScript.Steps.ToList())
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                if (NeedPerform(step) || force)
                    await step.Perform(cancellationToken);
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
            var needPerform = s.NextPerformTime <= DateTime.Now || !string.IsNullOrWhiteSpace(s.LastState);
            return needPerform;
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

        #region GUI

        public void CreateBackupScriptGui()
        {
            try
            {
                var backupName = string.Format(Resources.Str_BackupManager_BackupName,
                    Guid.NewGuid().ToString("N").Substring(0, 8));
                var backupPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                using (var selectPathWnd = new WndEnterPath(
                    Resources.Str_BackupManager_PleaseEnterName,
                    backupName,
                    backupPath))
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

        #endregion

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

        public void AddNewBackupScript(IBackupScript newBackupScript)
        {
            _dbWorker.Save(newBackupScript);
            _scripts.Add(newBackupScript);

            Notify();
        }
    }
}