using System;
using System.Windows.Forms;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using DataGuardian.Properties;

namespace DataGuardian.Windows
{
    public partial class WndMain : Form
    {
        private ICore Core => CoreStatic.Instance;

        public WndMain()
        {
            InitializeComponent();

            if (!DesignMode)
                Core.GuiManager.SetWindow(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetStartupState();
        }

        private void SetStartupState()
        {
            addToAutoStartToolStripMenuItem.CheckState = ShortcutHelper.CheckIfShortcutExist()
                ? CheckState.Checked
                : CheckState.Unchecked;
        }

        private void OnCmdAddProviderToolStripMenuItemClick(object sender, EventArgs e)
        {
            Core.CloudAccountsManager.AddAccount(this);
        }

        private void OnCmdEditProviderToolStripMenuItemClick(object sender, EventArgs e)
        {
            Core.CloudAccountsManager.RemoveAccount(ctlCloudAccounts.SelectedCloudProvider);
        }

        private void OnCmdExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnCmdCreateBackupScriptToolStripMenuItemClick(object sender, EventArgs e)
        {
            Core.BackupManager.CreateBackupScriptGui();
        }

        private void OnCmdEditBackupScriptToolStripMenuItem1Click(object sender, EventArgs e)
        {
            Core.BackupManager.EditBackupScriptGui(ctlBackupScripts.SelectedBackupScript);
        }

        private void OnCmdRemoveBackupScriptToolStripMenuItemClick(object sender, EventArgs e)
        {
            Core.BackupManager.RemoveBackupScriptGui(ctlBackupScripts.SelectedBackupScript);
        }

        private void OnCmdAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var wnd = new WndAbout())
                wnd.ShowDialog(this);
        }

        private void OnCmdAddToAutoStartToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShortcutHelper.ToggleAutoStartShortcut(Resources.ApplicationIcon);
            SetStartupState();
        }

        private void OnCmdShowRemoteBackupsToolStripMenuItemClick(object sender, EventArgs e)
        {
            Core.BackupManager.ShowRemoteBackupsGui(ctlBackupScripts.SelectedBackupScript);
        }
    }
}