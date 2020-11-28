
namespace DataGuardian.Controls
{
    partial class CtlBackupScripts
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
            this.tlpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvData = new DataGuardian.GUI.Controls.GuardianDataGridView();
            this.clmEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLastPerform = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLastState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctlFilter1 = new DataGuardian.Controls.CtlFilter();
            this.tlpRoot.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 2;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRoot.Controls.Add(this.tlpButtons, 1, 0);
            this.tlpRoot.Controls.Add(this.dgvData, 0, 1);
            this.tlpRoot.Controls.Add(this.ctlFilter1, 0, 0);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 2;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Size = new System.Drawing.Size(939, 446);
            this.tlpRoot.TabIndex = 0;
            // 
            // tlpButtons
            // 
            this.tlpButtons.Controls.Add(this.btnCreate);
            this.tlpButtons.Controls.Add(this.btnEdit);
            this.tlpButtons.Controls.Add(this.btnDelete);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.tlpButtons.Location = new System.Drawing.Point(469, 0);
            this.tlpButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.Size = new System.Drawing.Size(470, 40);
            this.tlpButtons.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.BackgroundImage = global::DataGuardian.Properties.Resources.Img_Plus;
            this.btnCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCreate.Location = new System.Drawing.Point(392, 3);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 34);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackgroundImage = global::DataGuardian.Properties.Resources.Img_Edit;
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEdit.Location = new System.Drawing.Point(311, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 34);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackgroundImage = global::DataGuardian.Properties.Resources.Img_Close;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Location = new System.Drawing.Point(230, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 34);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToOrderColumns = true;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmEnabled,
            this.clmCreateTime,
            this.clmTarget,
            this.clmName,
            this.clmLastPerform,
            this.clmLastState});
            this.tlpRoot.SetColumnSpan(this.dgvData, 2);
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 43);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersWidth = 62;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(933, 400);
            this.dgvData.TabIndex = 1;
            // 
            // clmEnabled
            // 
            this.clmEnabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmEnabled.FillWeight = 50F;
            this.clmEnabled.HeaderText = "Enabled";
            this.clmEnabled.MinimumWidth = 8;
            this.clmEnabled.Name = "clmEnabled";
            this.clmEnabled.ReadOnly = true;
            this.clmEnabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmEnabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clmCreateTime
            // 
            this.clmCreateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmCreateTime.FillWeight = 50F;
            this.clmCreateTime.HeaderText = "Create Time";
            this.clmCreateTime.MinimumWidth = 8;
            this.clmCreateTime.Name = "clmCreateTime";
            this.clmCreateTime.ReadOnly = true;
            // 
            // clmTarget
            // 
            this.clmTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmTarget.FillWeight = 150F;
            this.clmTarget.HeaderText = "Target";
            this.clmTarget.MinimumWidth = 8;
            this.clmTarget.Name = "clmTarget";
            this.clmTarget.ReadOnly = true;
            // 
            // clmName
            // 
            this.clmName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmName.HeaderText = "Name";
            this.clmName.MinimumWidth = 8;
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
            // 
            // clmLastPerform
            // 
            this.clmLastPerform.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmLastPerform.FillWeight = 80F;
            this.clmLastPerform.HeaderText = "Last Perform Time";
            this.clmLastPerform.MinimumWidth = 8;
            this.clmLastPerform.Name = "clmLastPerform";
            this.clmLastPerform.ReadOnly = true;
            // 
            // clmLastState
            // 
            this.clmLastState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmLastState.FillWeight = 50F;
            this.clmLastState.HeaderText = "Last State";
            this.clmLastState.MinimumWidth = 8;
            this.clmLastState.Name = "clmLastState";
            this.clmLastState.ReadOnly = true;
            // 
            // ctlFilter1
            // 
            this.ctlFilter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlFilter1.Location = new System.Drawing.Point(0, 0);
            this.ctlFilter1.Margin = new System.Windows.Forms.Padding(0);
            this.ctlFilter1.MaximumSize = new System.Drawing.Size(500, 40);
            this.ctlFilter1.MinimumSize = new System.Drawing.Size(200, 40);
            this.ctlFilter1.Name = "ctlFilter1";
            this.ctlFilter1.Size = new System.Drawing.Size(469, 40);
            this.ctlFilter1.TabIndex = 2;
            // 
            // CtlBackupScripts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpRoot);
            this.Name = "CtlBackupScripts";
            this.Size = new System.Drawing.Size(939, 446);
            this.tlpRoot.ResumeLayout(false);
            this.tlpButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private DataGuardian.GUI.Controls.GuardianDataGridView dgvData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLastPerform;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLastState;
        private System.Windows.Forms.FlowLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private CtlFilter ctlFilter1;
    }
}
