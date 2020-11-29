
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
            this.ctlPath = new DataGuardian.Controls.CtlPath();
            this.lsbPeriodParameters = new System.Windows.Forms.CheckedListBox();
            this.lblPeriodParameters = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lblRecurEvery = new System.Windows.Forms.Label();
            this.lblStartOfAction = new System.Windows.Forms.Label();
            this.nudRecurEvery = new System.Windows.Forms.NumericUpDown();
            this.lblActionParameter = new System.Windows.Forms.Label();
            this.txtActionParameter = new System.Windows.Forms.TextBox();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.lblSelectAccount = new System.Windows.Forms.Label();
            this.tlpRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecurEvery)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 4;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.68094F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.31906F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 166F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 234F));
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
            this.tlpRoot.Controls.Add(this.txtActionParameter, 2, 3);
            this.tlpRoot.Controls.Add(this.cmbAccount, 1, 3);
            this.tlpRoot.Controls.Add(this.lblSelectAccount, 1, 2);
            this.tlpRoot.Controls.Add(this.lblActionParameter, 2, 2);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.MaximumSize = new System.Drawing.Size(1600, 300);
            this.tlpRoot.MinimumSize = new System.Drawing.Size(800, 280);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 8;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRoot.Size = new System.Drawing.Size(800, 280);
            this.tlpRoot.TabIndex = 0;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.tlpRoot.SetColumnSpan(this.lblState, 4);
            this.lblState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblState.ForeColor = System.Drawing.Color.DarkRed;
            this.lblState.Location = new System.Drawing.Point(3, 241);
            this.lblState.Margin = new System.Windows.Forms.Padding(3);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(794, 36);
            this.lblState.TabIndex = 0;
            this.lblState.Text = "State";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbAction
            // 
            this.cmbAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAction.FormattingEnabled = true;
            this.cmbAction.Location = new System.Drawing.Point(3, 105);
            this.cmbAction.Name = "cmbAction";
            this.cmbAction.Size = new System.Drawing.Size(180, 28);
            this.cmbAction.TabIndex = 1;
            this.cmbAction.SelectedIndexChanged += new System.EventHandler(this.cmbAction_SelectedIndexChanged);
            // 
            // cmbPeriod
            // 
            this.cmbPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPeriod.FormattingEnabled = true;
            this.cmbPeriod.Location = new System.Drawing.Point(3, 177);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new System.Drawing.Size(180, 28);
            this.cmbPeriod.TabIndex = 1;
            this.cmbPeriod.SelectedIndexChanged += new System.EventHandler(this.cmbPeriod_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select action";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tlpRoot.SetColumnSpan(this.label3, 3);
            this.label3.Location = new System.Drawing.Point(3, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Latest state";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Select periodicity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tlpRoot.SetColumnSpan(this.label2, 3);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(559, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select path (or leave base path)";
            // 
            // ctlPath
            // 
            this.ctlPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRoot.SetColumnSpan(this.ctlPath, 4);
            this.ctlPath.Location = new System.Drawing.Point(3, 29);
            this.ctlPath.MaximumSize = new System.Drawing.Size(9999, 36);
            this.ctlPath.MinimumSize = new System.Drawing.Size(625, 36);
            this.ctlPath.Name = "ctlPath";
            this.ctlPath.SelectedPath = "";
            this.ctlPath.Size = new System.Drawing.Size(794, 36);
            this.ctlPath.TabIndex = 7;
            // 
            // lsbPeriodParameters
            // 
            this.lsbPeriodParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbPeriodParameters.FormattingEnabled = true;
            this.lsbPeriodParameters.Location = new System.Drawing.Point(568, 177);
            this.lsbPeriodParameters.Name = "lsbPeriodParameters";
            this.lsbPeriodParameters.Size = new System.Drawing.Size(229, 27);
            this.lsbPeriodParameters.TabIndex = 4;
            // 
            // lblPeriodParameters
            // 
            this.lblPeriodParameters.AutoSize = true;
            this.lblPeriodParameters.Location = new System.Drawing.Point(568, 144);
            this.lblPeriodParameters.Name = "lblPeriodParameters";
            this.lblPeriodParameters.Size = new System.Drawing.Size(139, 20);
            this.lblPeriodParameters.TabIndex = 6;
            this.lblPeriodParameters.Text = "Select parameters";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartTime.Location = new System.Drawing.Point(196, 178);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(193, 26);
            this.dtpStartTime.TabIndex = 3;
            // 
            // lblRecurEvery
            // 
            this.lblRecurEvery.AutoSize = true;
            this.lblRecurEvery.Location = new System.Drawing.Point(402, 144);
            this.lblRecurEvery.Name = "lblRecurEvery";
            this.lblRecurEvery.Size = new System.Drawing.Size(140, 20);
            this.lblRecurEvery.TabIndex = 6;
            this.lblRecurEvery.Text = "Recur every (days)";
            // 
            // lblStartOfAction
            // 
            this.lblStartOfAction.AutoSize = true;
            this.lblStartOfAction.Location = new System.Drawing.Point(189, 144);
            this.lblStartOfAction.Name = "lblStartOfAction";
            this.lblStartOfAction.Size = new System.Drawing.Size(155, 20);
            this.lblStartOfAction.TabIndex = 6;
            this.lblStartOfAction.Text = "Select start of action";
            // 
            // nudRecurEvery
            // 
            this.nudRecurEvery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRecurEvery.Location = new System.Drawing.Point(409, 178);
            this.nudRecurEvery.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.nudRecurEvery.Name = "nudRecurEvery";
            this.nudRecurEvery.Size = new System.Drawing.Size(146, 26);
            this.nudRecurEvery.TabIndex = 8;
            this.nudRecurEvery.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblActionParameter
            // 
            this.lblActionParameter.AutoSize = true;
            this.lblActionParameter.Location = new System.Drawing.Point(402, 72);
            this.lblActionParameter.Name = "lblActionParameter";
            this.lblActionParameter.Size = new System.Drawing.Size(125, 20);
            this.lblActionParameter.TabIndex = 6;
            this.lblActionParameter.Text = "Enter parameter";
            // 
            // txtActionParameter
            // 
            this.txtActionParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRoot.SetColumnSpan(this.txtActionParameter, 2);
            this.txtActionParameter.Location = new System.Drawing.Point(402, 106);
            this.txtActionParameter.Name = "txtActionParameter";
            this.txtActionParameter.Size = new System.Drawing.Size(395, 26);
            this.txtActionParameter.TabIndex = 5;
            // 
            // cmbAccount
            // 
            this.cmbAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(189, 105);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(207, 28);
            this.cmbAccount.TabIndex = 1;
            this.cmbAccount.SelectedIndexChanged += new System.EventHandler(this.cmbAction_SelectedIndexChanged);
            // 
            // lblSelectAccount
            // 
            this.lblSelectAccount.AutoSize = true;
            this.lblSelectAccount.Location = new System.Drawing.Point(189, 72);
            this.lblSelectAccount.Name = "lblSelectAccount";
            this.lblSelectAccount.Size = new System.Drawing.Size(117, 20);
            this.lblSelectAccount.TabIndex = 6;
            this.lblSelectAccount.Text = "Select Account";
            // 
            // CtlBackupStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tlpRoot);
            this.MaximumSize = new System.Drawing.Size(1318, 280);
            this.MinimumSize = new System.Drawing.Size(800, 280);
            this.Name = "CtlBackupStep";
            this.Size = new System.Drawing.Size(800, 276);
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
        private System.Windows.Forms.TextBox txtActionParameter;
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
    }
}
