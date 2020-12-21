
using DataGuardian.GUI.UserControls;

namespace DataGuardian.Windows
{
    partial class WndCreateEditBackupScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WndCreateEditBackupScript));
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddStep = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTargetPath = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.ctlPath = new DataGuardian.GUI.UserControls.CtlPath();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tlpRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.AutoScroll = true;
            this.tlpRoot.ColumnCount = 3;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tlpRoot.Controls.Add(this.btnAddStep, 1, 2);
            this.tlpRoot.Controls.Add(this.lblName, 0, 0);
            this.tlpRoot.Controls.Add(this.lblTargetPath, 0, 1);
            this.tlpRoot.Controls.Add(this.txtName, 1, 0);
            this.tlpRoot.Controls.Add(this.ctlPath, 1, 1);
            this.tlpRoot.Controls.Add(this.btnCancel, 0, 2);
            this.tlpRoot.Controls.Add(this.btnSave, 2, 2);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 3;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tlpRoot.Size = new System.Drawing.Size(856, 315);
            this.tlpRoot.TabIndex = 0;
            // 
            // btnAddStep
            // 
            this.btnAddStep.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddStep.Location = new System.Drawing.Point(354, 167);
            this.btnAddStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddStep.MaximumSize = new System.Drawing.Size(147, 32);
            this.btnAddStep.Name = "btnAddStep";
            this.btnAddStep.Size = new System.Drawing.Size(147, 32);
            this.btnAddStep.TabIndex = 0;
            this.btnAddStep.Text = "Add new step";
            this.btnAddStep.UseVisualStyleBackColor = true;
            this.btnAddStep.Click += new System.EventHandler(this.btnAddStep_Click);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 6);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(61, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTargetPath
            // 
            this.lblTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetPath.AutoSize = true;
            this.lblTargetPath.Location = new System.Drawing.Point(3, 32);
            this.lblTargetPath.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.lblTargetPath.Name = "lblTargetPath";
            this.lblTargetPath.Size = new System.Drawing.Size(61, 13);
            this.lblTargetPath.TabIndex = 1;
            this.lblTargetPath.Text = "TargetPath";
            this.lblTargetPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(71, 3);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(714, 20);
            this.txtName.TabIndex = 2;
            // 
            // ctlPath
            // 
            this.ctlPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPath.Location = new System.Drawing.Point(68, 27);
            this.ctlPath.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.ctlPath.MaximumSize = new System.Drawing.Size(6666, 23);
            this.ctlPath.MinimumSize = new System.Drawing.Size(417, 23);
            this.ctlPath.Name = "ctlPath";
            this.ctlPath.SelectedPath = "";
            this.ctlPath.Size = new System.Drawing.Size(720, 23);
            this.ctlPath.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(2, 277);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 6);
            this.btnCancel.MaximumSize = new System.Drawing.Size(147, 32);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 32);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(1181, 425);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.btnSave.MaximumSize = new System.Drawing.Size(220, 50);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 50);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // WndCreateEditBackupScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 315);
            this.Controls.Add(this.tlpRoot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(53339, 52014);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(872, 274);
            this.Name = "WndCreateEditBackupScript";
            this.Text = "Create/Edit script";
            this.tlpRoot.ResumeLayout(false);
            this.tlpRoot.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Button btnAddStep;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTargetPath;
        private System.Windows.Forms.TextBox txtName;
        private CtlPath ctlPath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}