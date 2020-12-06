using DataGuardian.GUI;
using DataGuardian.Plugins;
using DataGuardian.Windows;
using DataGuardian.Workers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGuardian.Controls;
using DataGuardian.GUI.Windows;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Plugins;
using DataGuardian.Properties;
using Timer = System.Windows.Forms.Timer;

namespace DataGuardian.Impl
{
    public class BackupManager : PluginBase, IBackupManager
    {
        private readonly List<IBackupScript> _scripts = new List<IBackupScript>();
        private readonly object _locker = new object();
        private bool _performingJob;
        private BackupDbWorker _dbWorker;
        private Timer _timer;
        private CancellationTokenSource _cancellationTokenSource;
        private WndRemoteBackups _viewRemoteBackupsWnd;

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
            _timer = new Timer {Interval = (int) TimeSpan.FromMinutes(1).TotalMilliseconds};
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

            foreach (var backupScript in GetBackupList())
            {
                try
                {
                    foreach (var backupStep in backupScript.Steps.ToList())
                    {
                        var needRestore = NeedRestoreStep(backupStep);
                        if (!needRestore)
                            continue;

                        Core.Logger.Log(string.Format(
                            Resources.Str_BackupManager_LocalVersionMissingLog,
                            backupScript.TargetPath,
                            backupScript.Name));

                        await PerformRestoreWithGui(backupScript, backupStep, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    Core.Logger.Log(Resources.Str_BackupManager_CronJobError, ex);
                }
            }

            foreach (var backupScript in GetBackupListToPerform())
            {
                try
                {
                    Core.Logger.Log(string.Format(Resources.Str_BackupManager_StartBackup, backupScript.Name));

                    await Perform(backupScript);
                }
                catch (Exception ex)
                {
                    Core.Logger.Log(Resources.Str_BackupManager_CronJobError, ex);
                }
            }

            lock (_locker)
                _performingJob = false;
        }

        private List<IBackupScript> GetBackupList()
        {
            return BackupScripts.Where(x => x.Enabled).ToList();
        }

        private List<IBackupScript> GetBackupListToPerform()
        {
            return BackupScripts.Where(x => x.Enabled && x.Steps.Any(NeedPerform)).ToList();
        }

        private static bool NeedRestoreStep(IBackupStep backupStep)
        {
            return backupStep.Action == BackupAction.BackupTo && !backupStep.CheckIfLocalCopyExists();
        }

        private async Task PerformRestoreWithGui(
            IBackupScript script,
            IBackupStep failed,
            CancellationToken cancellationToken)
        {
            var dialogResult = GuiHelper.ShowConfirmationDialog(
                string.Format(Resources.Str_BackupManager_LocalVersionMissing,
                    script.Name, failed.TargetPath, Environment.NewLine));

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
                    ? Resources.Str_BackupManager_DataRestoredSuccessfully
                    : Resources.Str_BackupManager_DataNotRestored);
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

        private static bool NeedPerform(IBackupStep s)
        {
            var needPerform = s.NextPerformTime <= DateTime.Now || !string.IsNullOrWhiteSpace(s.LastState);
            return needPerform;
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

                            CreateNewBackupScript(newBackupScript);

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
                    if (newBackupScript == null || backupScript.Equals(newBackupScript))
                        return;

                    EditBackupScript(backupScript, newBackupScript);

                    GuiHelper.ShowMessage("Backup script updated");
                }
            }
        }

        public async Task ShowRemoteBackupsGui(IBackupScript script)
        {
            if (script?.Steps == null)
                return;

            await ShowRemoteBackupsForBackupNames(script.Steps.Select(x => (x.Account, x.BackupFileName)));
        }

        public async Task ShowRemoteBackupsGui(IBackupStep step)
        {
            if (step == null)
                return;

            await ShowRemoteBackupsForBackupNames(new[] {(step.Account, step.BackupFileName)});
        }

        private async Task ShowRemoteBackupsForBackupNames(IEnumerable<(IAccount account, string fileName)> lst)
        {
            try
            {
                if (_viewRemoteBackupsWnd == null || _viewRemoteBackupsWnd.IsDisposed ||
                    _viewRemoteBackupsWnd.Disposing || !_viewRemoteBackupsWnd.IsHandleCreated)
                {
                    _viewRemoteBackupsWnd = new WndRemoteBackups(Core);
                    _viewRemoteBackupsWnd.Show();
                }
                else
                {
                    _viewRemoteBackupsWnd.WindowState = FormWindowState.Normal;
                    _viewRemoteBackupsWnd.BringToFront();
                    _viewRemoteBackupsWnd.Focus();
                }

                var states = new List<RemoteBackupsState>();
                foreach (var (account, fileName) in lst)
                {
                    var remoteBackupsState = await account.CloudStorageProvider.GetBackupState(account, fileName);
                    states.Add(remoteBackupsState);
                }

                _viewRemoteBackupsWnd.FillData(states);
            }
            catch (Exception ex)
            {
                Core.Logger.Log("Delete Backup Script", ex);
                GuiHelper.ShowMessage(ex);
            }
        }

        public void RemoveBackupScriptGui(IBackupScript script)
        {
            try
            {
                if (script == null)
                    return;

                RemoveBackupScript(script);
                GuiHelper.ShowMessage("Backup script removed");
            }
            catch (Exception ex)
            {
                Core.Logger.Log("Delete Backup Script", ex);
                GuiHelper.ShowMessage(ex);
            }
        }

        #endregion

        #region Non gui

        private void CreateNewBackupScript(IBackupScript newBackupScript)
        {
            _dbWorker.Save(newBackupScript);
            _scripts.Add(newBackupScript);

            Notify();
        }

        public void EditBackupScript(IBackupScript script, IBackupScript updatedScript)
        {
            _dbWorker.Edit(updatedScript);

            _scripts.Remove(script);
            _scripts.Add(updatedScript);

            Notify();
        }

        public void RemoveBackupScript(IBackupScript script)
        {
            try
            {
                if (script == null)
                    return;

                _dbWorker.Delete(script);
                _scripts.Remove(script);

                Notify();
            }
            catch (Exception ex)
            {
                Core.Logger.Log("RemoveBackupScript", ex);
            }
        }

        #endregion

        private void Notify()
        {
            BackupScriptsChanged?.Invoke(this, BackupScripts);
        }
    }
}