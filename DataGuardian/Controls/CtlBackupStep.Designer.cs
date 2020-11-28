
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
            this.txtActionParameter = new System.Windows.Forms.TextBox();
            this.cmbAction = new System.Windows.Forms.ComboBox();
            this.cmbPeriod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctlPath = new DataGuardian.Controls.CtlPath();
            this.lsbPeriodParameters = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudRecurEvery = new System.Windows.Forms.NumericUpDown();
            this.lblActionParameter = new System.Windows.Forms.Label();
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
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 232F));
            this.tlpRoot.Controls.Add(this.lblState, 0, 7);
            this.tlpRoot.Controls.Add(this.txtActionParameter, 1, 3);
            this.tlpRoot.Controls.Add(this.cmbAction, 0, 3);
            this.tlpRoot.Controls.Add(this.cmbPeriod, 0, 5);
            this.tlpRoot.Controls.Add(this.label1, 0, 2);
            this.tlpRoot.Controls.Add(this.label3, 0, 6);
            this.tlpRoot.Controls.Add(this.label4, 0, 4);
            this.tlpRoot.Controls.Add(this.label2, 0, 0);
            this.tlpRoot.Controls.Add(this.ctlPath, 0, 1);
            this.tlpRoot.Controls.Add(this.lsbPeriodParameters, 3, 5);
            this.tlpRoot.Controls.Add(this.label6, 3, 4);
            this.tlpRoot.Controls.Add(this.dtpStartTime, 1, 5);
            this.tlpRoot.Controls.Add(this.label7, 2, 4);
            this.tlpRoot.Controls.Add(this.label5, 1, 4);
            this.tlpRoot.Controls.Add(this.nudRecurEvery, 2, 5);
            this.tlpRoot.Controls.Add(this.lblActionParameter, 1, 2);
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
            // txtActionParameter
            // 
            this.txtActionParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRoot.SetColumnSpan(this.txtActionParameter, 3);
            this.txtActionParameter.Location = new System.Drawing.Point(190, 106);
            this.txtActionParameter.Name = "txtActionParameter";
            this.txtActionParameter.Size = new System.Drawing.Size(607, 26);
            this.txtActionParameter.TabIndex = 5;
            // 
            // cmbAction
            // 
            this.cmbAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAction.FormattingEnabled = true;
            this.cmbAction.Location = new System.Drawing.Point(3, 105);
            this.cmbAction.Name = "cmbAction";
            this.cmbAction.Size = new System.Drawing.Size(181, 28);
            this.cmbAction.TabIndex = 1;
            this.cmbAction.SelectedIndexChanged += new System.EventHandler(this.cmbAction_SelectedIndexChanged);
            // 
            // cmbPeriod
            // 
            this.cmbPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPeriod.FormattingEnabled = true;
            this.cmbPeriod.Location = new System.Drawing.Point(3, 177);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new System.Drawing.Size(181, 28);
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
            this.label2.Size = new System.Drawing.Size(561, 22);
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
            this.lsbPeriodParameters.Location = new System.Drawing.Point(570, 177);
            this.lsbPeriodParameters.Name = "lsbPeriodParameters";
            this.lsbPeriodParameters.Size = new System.Drawing.Size(227, 27);
            this.lsbPeriodParameters.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(570, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Select parameters";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartTime.Location = new System.Drawing.Point(197, 178);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(194, 26);
            this.dtpStartTime.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(404, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Recur every (days)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Select start of action";
            // 
            // nudRecurEvery
            // 
            this.nudRecurEvery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRecurEvery.Location = new System.Drawing.Point(411, 178);
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
            this.lblActionParameter.Location = new System.Drawing.Point(190, 72);
            this.lblActionParameter.Name = "lblActionParameter";
            this.lblActionParameter.Size = new System.Drawing.Size(125, 20);
            this.lblActionParameter.TabIndex = 6;
            this.lblActionParameter.Text = "Enter parameter";
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private CtlPath ctlPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudRecurEvery;
        private System.Windows.Forms.Label lblActionParameter;
    }
}
