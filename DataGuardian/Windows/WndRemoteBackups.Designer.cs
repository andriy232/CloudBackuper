
namespace DataGuardian.Windows
{
    partial class WndRemoteBackups
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WndRemoteBackups));
            this.lvData = new System.Windows.Forms.ListView();
            this.ctx = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdDeleteBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDeleteGroupBackups = new System.Windows.Forms.ToolStripMenuItem();
            this.ctx.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvData
            // 
            this.lvData.ContextMenuStrip = this.ctx;
            this.lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvData.FullRowSelect = true;
            this.lvData.HideSelection = false;
            this.lvData.Location = new System.Drawing.Point(0, 0);
            this.lvData.Name = "lvData";
            this.lvData.ShowItemToolTips = true;
            this.lvData.Size = new System.Drawing.Size(1180, 450);
            this.lvData.TabIndex = 0;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.View = System.Windows.Forms.View.Details;
            // 
            // ctx
            // 
            this.ctx.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdDeleteBackup,
            this.cmdDeleteGroupBackups});
            this.ctx.Name = "ctx";
            this.ctx.Size = new System.Drawing.Size(253, 68);
            // 
            // cmdDeleteBackup
            // 
            this.cmdDeleteBackup.Name = "cmdDeleteBackup";
            this.cmdDeleteBackup.Size = new System.Drawing.Size(252, 32);
            this.cmdDeleteBackup.Text = "Remove";
            this.cmdDeleteBackup.Click += new System.EventHandler(this.OnCmdDeleteBackupClick);
            // 
            // cmdDeleteGroupBackups
            // 
            this.cmdDeleteGroupBackups.Name = "cmdDeleteGroupBackups";
            this.cmdDeleteGroupBackups.Size = new System.Drawing.Size(252, 32);
            this.cmdDeleteGroupBackups.Text = "Remove all for group";
            this.cmdDeleteGroupBackups.Click += new System.EventHandler(this.OnCmdDeleteGroupBackupsClick);
            // 
            // WndRemoteBackups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 450);
            this.Controls.Add(this.lvData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WndRemoteBackups";
            this.Text = "Remote Backups";
            this.ctx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.ContextMenuStrip ctx;
        private System.Windows.Forms.ToolStripMenuItem cmdDeleteBackup;
        private System.Windows.Forms.ToolStripMenuItem cmdDeleteGroupBackups;
    }
}