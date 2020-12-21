
using DataGuardian.GUI.UserControls;

namespace DataGuardian.Controls
{
    partial class CtlBackupStep
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.lblState = new System.Windows.Forms.Label();
            this.cmbAction = new System.Windows.Forms.ComboBox();
            this.cmbPeriod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctlPath = new DataGuardian.GUI.UserControls.CtlPath();
            this.lsbPeriodParameters = new System.Windows.Forms.CheckedListBox();
            this.lblPeriodParameters = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lblRecurEvery = new System.Windows.Forms.Label();
            this.lblStartOfAction = new System.Windows.Forms.Label();
            this.nudRecurEvery = new System.Windows.Forms.NumericUpDown();
            this.txtBackupFileName = new System.Windows.Forms.TextBox();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.lblSelectAccount = new System.Windows.Forms.Label();
            this.lblActionParameter = new System.Windows.Forms.Label();
            this.ctlBackupPath = new DataGuardian.GUI.UserControls.CtlPath();
            this.lblEnterPath = new System.Windows.Forms.Label();
            this.tlpRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecurEvery)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 4;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.68094F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.31906F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.tlpRoot.Controls.Add(this.lblState, 0, 7);
            this.tlpRoot.Controls.Add(this.cmbAction, 0, 3);
            this.tlpRoot.Controls.Add(this.cmbPeriod, 0, 5);
            this.tlpRoot.Controls.Add(this.label1, 0, 2);
            this.tlpRoot.Controls.Add(this.label3, 0, 6);
            this.tlpRoot.Controls.Add(this.label4, 0, 4);
            this.tlpRoot.Controls.Add(this.label2, 0, 0);
            this.tlpRoot.Controls.Add(this.ctlPath, 0, 1);
            this.tlpRoot.Controls.Add(this.lsbPeriodParameters, 3, 5);
            this.tlpRoot.Controls.Add(this.lblPeriodParameters, 3, 4);
            this.tlpRoot.Controls.Add(this.dtpStartTime, 1, 5);
            this.tlpRoot.Controls.Add(this.lblRecurEvery, 2, 4);
            this.tlpRoot.Controls.Add(this.lblStartOfAction, 1, 4);
            this.tlpRoot.Controls.Add(this.nudRecurEvery, 2, 5);
            this.tlpRoot.Controls.Add(this.txtBackupFileName, 2, 3);
            this.tlpRoot.Controls.Add(this.cmbAccount, 1, 3);
            this.tlpRoot.Controls.Add(this.lblSelectAccount, 1, 2);
            this.tlpRoot.Controls.Add(this.lblActionParameter, 2, 2);
            this.tlpRoot.Controls.Add(this.ctlBackupPath, 3, 3);
            this.tlpRoot.Controls.Add(this.lblEnterPath, 3, 2);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 8;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpRoot.Size = new System.Drawing.Size(687, 195);
            this.tlpRoot.TabIndex = 0;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.tlpRoot.SetColumnSpan(this.lblState, 4);
            this.lblState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblState.ForeColor = System.Drawing.Color.DarkRed;
            this.lblState.Location = new System.Drawing.Point(2, 169);
            this.lblState.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(683, 24);
            this.lblState.TabIndex = 0;
            this.lblState.Text = "State";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbAction
            // 
            this.cmbAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAction.FormattingEnabled = true;
            this.cmbAction.Location = new System.Drawing.Point(2, 73);
            this.cmbAction.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbAction.Name = "cmbAction";
            this.cmbAction.Size = new System.Drawing.Size(95, 21);
            this.cmbAction.TabIndex = 1;
            this.cmbAction.SelectedIndexChanged += new System.EventHandler(this.OnCmbActionSelectedIndexChanged);
            // 
            // cmbPeriod
            // 
            this.cmbPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPeriod.FormattingEnabled = true;
            this.cmbPeriod.Location = new System.Drawing.Point(2, 124);
            this.cmbPeriod.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new System.Drawing.Size(95, 21);
            this.cmbPeriod.TabIndex = 1;
            this.cmbPeriod.SelectedIndexChanged += new System.EventHandler(this.OnCmbPeriodSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select action";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tlpRoot.SetColumnSpan(this.label3, 3);
            this.label3.Location = new System.Drawing.Point(2, 153);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Latest state";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Select periodicity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tlpRoot.SetColumnSpan(this.label2, 3);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(2, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(439, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select path (or leave base path)";
            // 
            // ctlPath
            // 
            this.ctlPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRoot.SetColumnSpan(this.ctlPath, 4);
            this.ctlPath.Location = new System.Drawing.Point(1, 21);
            this.ctlPath.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.ctlPath.MaximumSize = new System.Drawing.Size(6666, 23);
            this.ctlPath.MinimumSize = new System.Drawing.Size(417, 23);
            this.ctlPath.Name = "ctlPath";
            this.ctlPath.SelectedPath = "";
            this.ctlPath.Size = new System.Drawing.Size(685, 23);
            this.ctlPath.TabIndex = 7;
            // 
            // lsbPeriodParameters
            // 
            this.lsbPeriodParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbPeriodParameters.CheckOnClick = true;
            this.lsbPeriodParameters.FormattingEnabled = true;
            this.lsbPeriodParameters.Location = new System.Drawing.Point(445, 118);
            this.lsbPeriodParameters.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lsbPeriodParameters.Name = "lsbPeriodParameters";
            this.lsbPeriodParameters.Size = new System.Drawing.Size(240, 19);
            this.lsbPeriodParameters.TabIndex = 4;
            // 
            // lblPeriodParameters
            // 
            this.lblPeriodParameters.AutoSize = true;
            this.lblPeriodParameters.Location = new System.Drawing.Point(445, 102);
            this.lblPeriodParameters.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPeriodParameters.Name = "lblPeriodParameters";
            this.lblPeriodParameters.Size = new System.Drawing.Size(92, 13);
            this.lblPeriodParameters.TabIndex = 6;
            this.lblPeriodParameters.Text = "Select parameters";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartTime.Location = new System.Drawing.Point(106, 124);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(100, 20);
            this.dtpStartTime.TabIndex = 3;
            // 
            // lblRecurEvery
            // 
            this.lblRecurEvery.AutoSize = true;
            this.lblRecurEvery.Location = new System.Drawing.Point(215, 102);
            this.lblRecurEvery.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecurEvery.Name = "lblRecurEvery";
            this.lblRecurEvery.Size = new System.Drawing.Size(96, 13);
            this.lblRecurEvery.TabIndex = 6;
            this.lblRecurEvery.Text = "Recur every (days)";
            // 
            // lblStartOfAction
            // 
            this.lblStartOfAction.AutoSize = true;
            this.lblStartOfAction.Location = new System.Drawing.Point(101, 102);
            this.lblStartOfAction.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStartOfAction.Name = "lblStartOfAction";
            this.lblStartOfAction.Size = new System.Drawing.Size(104, 13);
            this.lblStartOfAction.TabIndex = 6;
            this.lblStartOfAction.Text = "Select start of action";
            // 
            // nudRecurEvery
            // 
            this.nudRecurEvery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRecurEvery.Location = new System.Drawing.Point(220, 124);
            this.nudRecurEvery.Margin = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.nudRecurEvery.Name = "nudRecurEvery";
            this.nudRecurEvery.Size = new System.Drawing.Size(216, 20);
            this.nudRecurEvery.TabIndex = 8;
            this.nudRecurEvery.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtBackupFileName
            // 
            this.txtBackupFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackupFileName.Location = new System.Drawing.Point(215, 73);
            this.txtBackupFileName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBackupFileName.Name = "txtBackupFileName";
            this.txtBackupFileName.Size = new System.Drawing.Size(226, 20);
            this.txtBackupFileName.TabIndex = 5;
            // 
            // cmbAccount
            // 
            this.cmbAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(101, 73);
            this.cmbAccount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(110, 21);
            this.cmbAccount.TabIndex = 1;
            // 
            // lblSelectAccount
            // 
            this.lblSelectAccount.AutoSize = true;
            this.lblSelectAccount.Location = new System.Drawing.Point(101, 51);
            this.lblSelectAccount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelectAccount.Name = "lblSelectAccount";
            this.lblSelectAccount.Size = new System.Drawing.Size(80, 13);
            this.lblSelectAccount.TabIndex = 6;
            this.lblSelectAccount.Text = "Select Account";
            // 
            // lblActionParameter
            // 
            this.lblActionParameter.AutoSize = true;
            this.lblActionParameter.Location = new System.Drawing.Point(215, 51);
            this.lblActionParameter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblActionParameter.Name = "lblActionParameter";
            this.lblActionParameter.Size = new System.Drawing.Size(82, 13);
            this.lblActionParameter.TabIndex = 6;
            this.lblActionParameter.Text = "Enter parameter";
            // 
            // ctlBackupPath
            // 
            this.ctlBackupPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlBackupPath.Location = new System.Drawing.Point(444, 72);
            this.ctlBackupPath.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.ctlBackupPath.MaximumSize = new System.Drawing.Size(6666, 23);
            this.ctlBackupPath.MinimumSize = new System.Drawing.Size(0, 23);
            this.ctlBackupPath.Name = "ctlBackupPath";
            this.ctlBackupPath.SelectedPath = "";
            this.ctlBackupPath.Size = new System.Drawing.Size(242, 23);
            this.ctlBackupPath.TabIndex = 7;
            // 
            // lblEnterPath
            // 
            this.lblEnterPath.AutoSize = true;
            this.lblEnterPath.Location = new System.Drawing.Point(445, 51);
            this.lblEnterPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEnterPath.Name = "lblEnterPath";
            this.lblEnterPath.Size = new System.Drawing.Size(56, 13);
            this.lblEnterPath.TabIndex = 6;
            this.lblEnterPath.Text = "Enter path";
            // 
            // CtlBackupStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tlpRoot);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(880, 229);
            this.MinimumSize = new System.Drawing.Size(688, 196);
            this.Name = "CtlBackupStep";
            this.Size = new System.Drawing.Size(687, 195);
            this.tlpRoot.ResumeLayout(false);
            this.tlpRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecurEvery)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.ComboBox cmbAction;
        private System.Windows.Forms.ComboBox cmbPeriod;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.CheckedListBox lsbPeriodParameters;
        private System.Windows.Forms.TextBox txtBackupFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStartOfAction;
        private System.Windows.Forms.Label lblPeriodParameters;
        private CtlPath ctlPath;
        private System.Windows.Forms.Label lblRecurEvery;
        private System.Windows.Forms.NumericUpDown nudRecurEvery;
        private System.Windows.Forms.Label lblActionParameter;
        private System.Windows.Forms.ComboBox cmbAccount;
        private System.Windows.Forms.Label lblSelectAccount;
        private CtlPath ctlBackupPath;
        private System.Windows.Forms.Label lblEnterPath;
    }
}
