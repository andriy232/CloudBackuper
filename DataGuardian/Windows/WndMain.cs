using System;
using System.Windows.Forms;
using DataGuardian.Plugins.Core;
using DataGuardian.Properties;

namespace DataGuardian.Windows
{
    public partial class WndMain : Form
    {
        public WndMain()
        {
            InitializeComponent();
        }

        private void addProviderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoreStatic.Instance.CloudAccountsManager.AddAccount(this);
        }

        private void editProviderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedCloudProvider = ctlCloudAccounts.SelectedCloudProvider;
            if (selectedCloudProvider != null)
                CoreStatic.Instance.CloudAccountsManager.RemoveAccount(selectedCloudProvider);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void createBackupScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoreStatic.Instance.BackupManager.CreateBackupScriptGui();
        }

        private void editBackupScriptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var selectedBackupScript = ctlBackupScripts.SelectedBackupScript;
            if (selectedBackupScript != null)
                CoreStatic.Instance.BackupManager.EditBackupScriptGui(selectedBackupScript);
        }

        private void removeBackupScriptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var selectedBackupScript = ctlBackupScripts.SelectedBackupScript;
            if (selectedBackupScript != null)
                CoreStatic.Instance.BackupManager.RemoveBackupScript(selectedBackupScript);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var wnd = new WndAbout())
                wnd.ShowDialog(this);
        }
    }
}