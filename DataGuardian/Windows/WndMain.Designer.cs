
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
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToAutoStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctlCloudAccounts1 = new DataGuardian.Controls.CtlCloudAccounts();
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
            this.splRoot.Location = new System.Drawing.Point(0, 33);
            this.splRoot.Name = "splRoot";
            // 
            // splRoot.Panel1
            // 
            this.splRoot.Panel1.Controls.Add(this.ctlCloudAccounts1);
            // 
            // splRoot.Panel2
            // 
            this.splRoot.Panel2.Controls.Add(this.splChild);
            this.splRoot.Size = new System.Drawing.Size(1233, 700);
            this.splRoot.SplitterDistance = 410;
            this.splRoot.TabIndex = 0;
            // 
            // splChild
            // 
            this.splChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splChild.Location = new System.Drawing.Point(0, 0);
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
            this.splChild.Size = new System.Drawing.Size(819, 700);
            this.splChild.SplitterDistance = 512;
            this.splChild.TabIndex = 0;
            // 
            // ctlBackupScripts
            // 
            this.ctlBackupScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlBackupScripts.Location = new System.Drawing.Point(0, 0);
            this.ctlBackupScripts.Name = "ctlBackupScripts";
            this.ctlBackupScripts.SelectedBackupScript = null;
            this.ctlBackupScripts.Size = new System.Drawing.Size(819, 512);
            this.ctlBackupScripts.TabIndex = 0;
            // 
            // ctlLog
            // 
            this.ctlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlLog.Location = new System.Drawing.Point(0, 0);
            this.ctlLog.Name = "ctlLog";
            this.ctlLog.Size = new System.Drawing.Size(819, 184);
            this.ctlLog.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainToolStripMenuItem,
            this.backupToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1233, 33);
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
            this.mainToolStripMenuItem.Size = new System.Drawing.Size(67, 29);
            this.mainToolStripMenuItem.Text = "Main";
            // 
            // addProviderToolStripMenuItem
            // 
            this.addProviderToolStripMenuItem.Name = "addProviderToolStripMenuItem";
            this.addProviderToolStripMenuItem.Size = new System.Drawing.Size(299, 34);
            this.addProviderToolStripMenuItem.Text = "Add cloud provider";
            this.addProviderToolStripMenuItem.Click += new System.EventHandler(this.addProviderToolStripMenuItem_Click);
            // 
            // editProviderToolStripMenuItem
            // 
            this.editProviderToolStripMenuItem.Name = "editProviderToolStripMenuItem";
            this.editProviderToolStripMenuItem.Size = new System.Drawing.Size(299, 34);
            this.editProviderToolStripMenuItem.Text = "Remove cloud provider";
            this.editProviderToolStripMenuItem.Click += new System.EventHandler(this.editProviderToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(299, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createBackupScriptToolStripMenuItem,
            this.editBackupScriptToolStripMenuItem1,
            this.removeBackupScriptToolStripMenuItem1});
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(85, 29);
            this.backupToolStripMenuItem.Text = "Backup";
            // 
            // createBackupScriptToolStripMenuItem
            // 
            this.createBackupScriptToolStripMenuItem.Name = "createBackupScriptToolStripMenuItem";
            this.createBackupScriptToolStripMenuItem.Size = new System.Drawing.Size(289, 34);
            this.createBackupScriptToolStripMenuItem.Text = "Create backup script";
            this.createBackupScriptToolStripMenuItem.Click += new System.EventHandler(this.createBackupScriptToolStripMenuItem_Click);
            // 
            // editBackupScriptToolStripMenuItem1
            // 
            this.editBackupScriptToolStripMenuItem1.Name = "editBackupScriptToolStripMenuItem1";
            this.editBackupScriptToolStripMenuItem1.Size = new System.Drawing.Size(289, 34);
            this.editBackupScriptToolStripMenuItem1.Text = "Edit backup script";
            this.editBackupScriptToolStripMenuItem1.Click += new System.EventHandler(this.editBackupScriptToolStripMenuItem1_Click);
            // 
            // removeBackupScriptToolStripMenuItem1
            // 
            this.removeBackupScriptToolStripMenuItem1.Name = "removeBackupScriptToolStripMenuItem1";
            this.removeBackupScriptToolStripMenuItem1.Size = new System.Drawing.Size(289, 34);
            this.removeBackupScriptToolStripMenuItem1.Text = "Remove backup script";
            this.removeBackupScriptToolStripMenuItem1.Click += new System.EventHandler(this.removeBackupScriptToolStripMenuItem1_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToAutoStartToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(118, 29);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // addToAutoStartToolStripMenuItem
            // 
            this.addToAutoStartToolStripMenuItem.Name = "addToAutoStartToolStripMenuItem";
            this.addToAutoStartToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.addToAutoStartToolStripMenuItem.Text = "Add to AutoStart";
            this.addToAutoStartToolStripMenuItem.Click += new System.EventHandler(this.addToAutoStartToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ctlCloudAccounts1
            // 
            this.ctlCloudAccounts1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlCloudAccounts1.Location = new System.Drawing.Point(0, 0);
            this.ctlCloudAccounts1.Name = "ctlCloudAccounts1";
            this.ctlCloudAccounts1.SelectedCloudProvider = null;
            this.ctlCloudAccounts1.Size = new System.Drawing.Size(410, 700);
            this.ctlCloudAccounts1.TabIndex = 0;
            // 
            // WndMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 733);
            this.Controls.Add(this.splRoot);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1100, 368);
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
        private CtlCloudAccounts ctlCloudAccounts;
        private CtlLog ctlLog;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem addToAutoStartToolStripMenuItem;
        private CtlCloudAccounts ctlCloudAccounts1;
    }
}

