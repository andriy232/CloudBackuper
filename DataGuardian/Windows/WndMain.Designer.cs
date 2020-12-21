
using DataGuardian.Controls;

namespace DataGuardian.Windows
{
    partial class WndMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WndMain));
            this.splRoot = new System.Windows.Forms.SplitContainer();
            this.ctlCloudAccounts = new DataGuardian.Controls.CtlCloudAccounts();
            this.splChild = new System.Windows.Forms.SplitContainer();
            this.ctlBackupScripts = new DataGuardian.Controls.CtlBackupScripts();
            this.ctlLog = new DataGuardian.Controls.CtlLog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProviderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editProviderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBackupScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editBackupScriptToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeBackupScriptToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showRemoteBackupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToAutoStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splRoot)).BeginInit();
            this.splRoot.Panel1.SuspendLayout();
            this.splRoot.Panel2.SuspendLayout();
            this.splRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splChild)).BeginInit();
            this.splChild.Panel1.SuspendLayout();
            this.splChild.Panel2.SuspendLayout();
            this.splChild.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splRoot
            // 
            this.splRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splRoot.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splRoot.IsSplitterFixed = true;
            this.splRoot.Location = new System.Drawing.Point(0, 24);
            this.splRoot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splRoot.Name = "splRoot";
            // 
            // splRoot.Panel1
            // 
            this.splRoot.Panel1.Controls.Add(this.ctlCloudAccounts);
            // 
            // splRoot.Panel2
            // 
            this.splRoot.Panel2.Controls.Add(this.splChild);
            this.splRoot.Size = new System.Drawing.Size(1039, 452);
            this.splRoot.SplitterDistance = 260;
            this.splRoot.SplitterWidth = 3;
            this.splRoot.TabIndex = 0;
            // 
            // ctlCloudAccounts
            // 
            this.ctlCloudAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlCloudAccounts.Location = new System.Drawing.Point(0, 0);
            this.ctlCloudAccounts.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.ctlCloudAccounts.Name = "ctlCloudAccounts";
            this.ctlCloudAccounts.Padding = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.ctlCloudAccounts.SelectedCloudProvider = null;
            this.ctlCloudAccounts.Size = new System.Drawing.Size(260, 452);
            this.ctlCloudAccounts.TabIndex = 0;
            // 
            // splChild
            // 
            this.splChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splChild.Location = new System.Drawing.Point(0, 0);
            this.splChild.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splChild.Name = "splChild";
            this.splChild.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splChild.Panel1
            // 
            this.splChild.Panel1.Controls.Add(this.ctlBackupScripts);
            // 
            // splChild.Panel2
            // 
            this.splChild.Panel2.Controls.Add(this.ctlLog);
            this.splChild.Size = new System.Drawing.Size(776, 452);
            this.splChild.SplitterDistance = 300;
            this.splChild.SplitterWidth = 3;
            this.splChild.TabIndex = 0;
            // 
            // ctlBackupScripts
            // 
            this.ctlBackupScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlBackupScripts.Location = new System.Drawing.Point(0, 0);
            this.ctlBackupScripts.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.ctlBackupScripts.Name = "ctlBackupScripts";
            this.ctlBackupScripts.Padding = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.ctlBackupScripts.SelectedBackupScript = null;
            this.ctlBackupScripts.Size = new System.Drawing.Size(776, 300);
            this.ctlBackupScripts.TabIndex = 0;
            // 
            // ctlLog
            // 
            this.ctlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlLog.Location = new System.Drawing.Point(0, 0);
            this.ctlLog.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.ctlLog.Name = "ctlLog";
            this.ctlLog.Padding = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.ctlLog.Size = new System.Drawing.Size(776, 149);
            this.ctlLog.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.backupToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1039, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainToolStripMenuItem
            // 
            this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProviderToolStripMenuItem,
            this.editProviderToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(46, 22);
            this.mainToolStripMenuItem.Text = "Main";
            // 
            // addProviderToolStripMenuItem
            // 
            this.addProviderToolStripMenuItem.Name = "addProviderToolStripMenuItem";
            this.addProviderToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.addProviderToolStripMenuItem.Text = "Add cloud provider";
            this.addProviderToolStripMenuItem.Click += new System.EventHandler(this.OnCmdAddProviderToolStripMenuItemClick);
            // 
            // editProviderToolStripMenuItem
            // 
            this.editProviderToolStripMenuItem.Name = "editProviderToolStripMenuItem";
            this.editProviderToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.editProviderToolStripMenuItem.Text = "Remove cloud provider";
            this.editProviderToolStripMenuItem.Click += new System.EventHandler(this.OnCmdEditProviderToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnCmdExitToolStripMenuItemClick);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createBackupScriptToolStripMenuItem,
            this.editBackupScriptToolStripMenuItem1,
            this.removeBackupScriptToolStripMenuItem1,
            this.showRemoteBackupsToolStripMenuItem});
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(58, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            // 
            // createBackupScriptToolStripMenuItem
            // 
            this.createBackupScriptToolStripMenuItem.Name = "createBackupScriptToolStripMenuItem";
            this.createBackupScriptToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.createBackupScriptToolStripMenuItem.Text = "Create backup script";
            this.createBackupScriptToolStripMenuItem.Click += new System.EventHandler(this.OnCmdCreateBackupScriptToolStripMenuItemClick);
            // 
            // editBackupScriptToolStripMenuItem1
            // 
            this.editBackupScriptToolStripMenuItem1.Name = "editBackupScriptToolStripMenuItem1";
            this.editBackupScriptToolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
            this.editBackupScriptToolStripMenuItem1.Text = "Edit backup script";
            this.editBackupScriptToolStripMenuItem1.Click += new System.EventHandler(this.OnCmdEditBackupScriptToolStripMenuItem1Click);
            // 
            // removeBackupScriptToolStripMenuItem1
            // 
            this.removeBackupScriptToolStripMenuItem1.Name = "removeBackupScriptToolStripMenuItem1";
            this.removeBackupScriptToolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
            this.removeBackupScriptToolStripMenuItem1.Text = "Remove backup script";
            this.removeBackupScriptToolStripMenuItem1.Click += new System.EventHandler(this.OnCmdRemoveBackupScriptToolStripMenuItemClick);
            // 
            // showRemoteBackupsToolStripMenuItem
            // 
            this.showRemoteBackupsToolStripMenuItem.Name = "showRemoteBackupsToolStripMenuItem";
            this.showRemoteBackupsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.showRemoteBackupsToolStripMenuItem.Text = "Show remote backups";
            this.showRemoteBackupsToolStripMenuItem.Click += new System.EventHandler(this.OnCmdShowRemoteBackupsToolStripMenuItemClick);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToAutoStartToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // addToAutoStartToolStripMenuItem
            // 
            this.addToAutoStartToolStripMenuItem.Name = "addToAutoStartToolStripMenuItem";
            this.addToAutoStartToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.addToAutoStartToolStripMenuItem.Text = "Add to AutoStart";
            this.addToAutoStartToolStripMenuItem.Click += new System.EventHandler(this.OnCmdAddToAutoStartToolStripMenuItemClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnCmdAboutToolStripMenuItemClick);
            // 
            // WndMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 476);
            this.Controls.Add(this.splRoot);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(739, 253);
            this.Name = "WndMain";
            this.Text = "DataGuardian";
            this.splRoot.Panel1.ResumeLayout(false);
            this.splRoot.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splRoot)).EndInit();
            this.splRoot.ResumeLayout(false);
            this.splChild.Panel1.ResumeLayout(false);
            this.splChild.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splChild)).EndInit();
            this.splChild.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splRoot;
        private System.Windows.Forms.SplitContainer splChild;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addProviderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editProviderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createBackupScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editBackupScriptToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeBackupScriptToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private CtlBackupScripts ctlBackupScripts;
        private CtlLog ctlLog;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem addToAutoStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showRemoteBackupsToolStripMenuItem;
        private CtlCloudAccounts ctlCloudAccounts;
    }
}

