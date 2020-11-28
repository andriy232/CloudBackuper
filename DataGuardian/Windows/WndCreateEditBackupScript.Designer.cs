
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
            this.ctlPath1 = new DataGuardian.Controls.CtlPath();
            this.tlpRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.AutoScroll = true;
            this.tlpRoot.ColumnCount = 3;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpRoot.Controls.Add(this.btnAddStep, 1, 2);
            this.tlpRoot.Controls.Add(this.lblName, 0, 0);
            this.tlpRoot.Controls.Add(this.lblTargetPath, 0, 1);
            this.tlpRoot.Controls.Add(this.txtName, 1, 0);
            this.tlpRoot.Controls.Add(this.ctlPath1, 1, 1);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 3;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 217F));
            this.tlpRoot.Size = new System.Drawing.Size(1178, 554);
            this.tlpRoot.TabIndex = 0;
            // 
            // btnAddStep
            // 
            this.btnAddStep.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddStep.Location = new System.Drawing.Point(502, 292);
            this.btnAddStep.MaximumSize = new System.Drawing.Size(220, 50);
            this.btnAddStep.Name = "btnAddStep";
            this.btnAddStep.Size = new System.Drawing.Size(220, 50);
            this.btnAddStep.TabIndex = 0;
            this.btnAddStep.Text = "Add new step";
            this.btnAddStep.UseVisualStyleBackColor = true;
            this.btnAddStep.Click += new System.EventHandler(this.btnAddStep_Click);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 10);
            this.lblName.Margin = new System.Windows.Forms.Padding(5);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(117, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTargetPath
            // 
            this.lblTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetPath.AutoSize = true;
            this.lblTargetPath.Location = new System.Drawing.Point(5, 50);
            this.lblTargetPath.Margin = new System.Windows.Forms.Padding(5);
            this.lblTargetPath.Name = "lblTargetPath";
            this.lblTargetPath.Size = new System.Drawing.Size(117, 20);
            this.lblTargetPath.TabIndex = 1;
            this.lblTargetPath.Text = "TargetPath";
            this.lblTargetPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(133, 7);
            this.txtName.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(959, 26);
            this.txtName.TabIndex = 2;
            // 
            // ctlPath1
            // 
            this.ctlPath1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPath1.Location = new System.Drawing.Point(130, 43);
            this.ctlPath1.MaximumSize = new System.Drawing.Size(9999, 36);
            this.ctlPath1.MinimumSize = new System.Drawing.Size(625, 36);
            this.ctlPath1.Name = "ctlPath1";
            this.ctlPath1.SelectedPath = "";
            this.ctlPath1.Size = new System.Drawing.Size(965, 36);
            this.ctlPath1.TabIndex = 3;
            // 
            // WndCreateEditBackupScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 554);
            this.Controls.Add(this.tlpRoot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1200, 80000);
            this.MinimumSize = new System.Drawing.Size(1200, 400);
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
        private Controls.CtlPath ctlPath1;
    }
}