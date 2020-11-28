
namespace DataGuardian.Windows
{
    partial class WndEnterPath
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WndEnterPath));
            this.ctlPath = new DataGuardian.Controls.CtlPath();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ctlPath
            // 
            this.ctlPath.Location = new System.Drawing.Point(12, 161);
            this.ctlPath.MaximumSize = new System.Drawing.Size(9999, 36);
            this.ctlPath.MinimumSize = new System.Drawing.Size(625, 36);
            this.ctlPath.Name = "ctlPath";
            this.ctlPath.SelectedPath = "";
            this.ctlPath.Size = new System.Drawing.Size(776, 36);
            this.ctlPath.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(776, 103);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Please enter name and path for your backup script, you will be able to copy, arch" +
    "ive, upload your data into cloud storage ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.Location = new System.Drawing.Point(648, 216);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(140, 40);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(17, 115);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(763, 26);
            this.txtName.TabIndex = 3;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // WndEnterPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 268);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ctlPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(822, 324);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(822, 324);
            this.Name = "WndEnterPath";
            this.Text = "WndEnterPath";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtlPath ctlPath;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtName;
    }
}