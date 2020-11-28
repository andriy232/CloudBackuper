
namespace DataGuardian.Controls
{
    partial class CtlLog
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
            this.dgvData = new DataGuardian.GUI.Controls.GuardianDataGridView();
            this.clmTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmTime,
            this.clmSource,
            this.clmMessage});
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Margin = new System.Windows.Forms.Padding(6);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowHeadersWidth = 62;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(969, 468);
            this.dgvData.TabIndex = 0;
            // 
            // clmTime
            // 
            this.clmTime.HeaderText = "Time";
            this.clmTime.MinimumWidth = 8;
            this.clmTime.Name = "clmTime";
            this.clmTime.ReadOnly = true;
            this.clmTime.Width = 150;
            // 
            // clmSource
            // 
            this.clmSource.HeaderText = "Source";
            this.clmSource.MinimumWidth = 8;
            this.clmSource.Name = "clmSource";
            this.clmSource.ReadOnly = true;
            this.clmSource.Width = 200;
            // 
            // clmMessage
            // 
            this.clmMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmMessage.HeaderText = "Message";
            this.clmMessage.MinimumWidth = 8;
            this.clmMessage.Name = "clmMessage";
            this.clmMessage.ReadOnly = true;
            // 
            // CtlLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvData);
            this.Name = "CtlLog";
            this.Size = new System.Drawing.Size(969, 468);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGuardian.GUI.Controls.GuardianDataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMessage;
    }
}
