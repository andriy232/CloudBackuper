
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

            cmdDelete.Click -= OnCmdDelete;
            cmdEdit.Click -= OnCmdEdit;
            cmdToggleDisable.Click -= OnCmdDisable;
            cmdPerformNow.Click -= OnCmdPerform;
            ctlFilter.FilterChanged -= OnFilterChanged;
            if(Core?.BackupManager!=null)
            Core.BackupManager.BackupScriptsChanged -= OnBackupScriptsChanged;
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtlBackupScripts));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.tlpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvData = new DataGuardian.GUI.Controls.GuardianDataGridView();
            this.clmCurrentState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLastPerform = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNextPerformTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLastState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxScript = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdToggleDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdPerformNow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctlFilter = new DataGuardian.Controls.CtlFilter();
            this.gtbRoot = new System.Windows.Forms.GroupBox();
            this.tlpRoot.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.ctxScript.SuspendLayout();
            this.gtbRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 2;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRoot.Controls.Add(this.tlpButtons, 1, 0);
            this.tlpRoot.Controls.Add(this.dgvData, 0, 1);
            this.tlpRoot.Controls.Add(this.ctlFilter, 0, 0);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(3, 22);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 2;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Size = new System.Drawing.Size(978, 452);
            this.tlpRoot.TabIndex = 0;
            // 
            // tlpButtons
            // 
            this.tlpButtons.Controls.Add(this.btnDelete);
            this.tlpButtons.Controls.Add(this.btnEdit);
            this.tlpButtons.Controls.Add(this.btnCreate);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.tlpButtons.Location = new System.Drawing.Point(489, 0);
            this.tlpButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.Size = new System.Drawing.Size(489, 40);
            this.tlpButtons.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreate.BackgroundImage")));
            this.btnCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCreate.Location = new System.Drawing.Point(249, 3);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 34);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEdit.Location = new System.Drawing.Point(330, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 34);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.OnBtnEditClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Location = new System.Drawing.Point(411, 3);
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
            this.clmCurrentState,
            this.clmEnabled,
            this.clmName,
            this.clmTarget,
            this.clmCreateTime,
            this.clmLastPerform,
            this.clmNextPerformTime,
            this.clmLastState});
            this.tlpRoot.SetColumnSpan(this.dgvData, 2);
            this.dgvData.ContextMenuStrip = this.ctxScript;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 43);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowHeadersWidth = 62;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(972, 406);
            this.dgvData.TabIndex = 1;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellClick);
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentDoubleClick);
            // 
            // clmCurrentState
            // 
            this.clmCurrentState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmCurrentState.FillWeight = 50F;
            this.clmCurrentState.HeaderText = "Current State";
            this.clmCurrentState.MinimumWidth = 8;
            this.clmCurrentState.Name = "clmCurrentState";
            this.clmCurrentState.ReadOnly = true;
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
            // clmName
            // 
            this.clmName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmName.HeaderText = "Name";
            this.clmName.MinimumWidth = 8;
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
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
            // clmCreateTime
            // 
            this.clmCreateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmCreateTime.FillWeight = 50F;
            this.clmCreateTime.HeaderText = "Created Time";
            this.clmCreateTime.MinimumWidth = 8;
            this.clmCreateTime.Name = "clmCreateTime";
            this.clmCreateTime.ReadOnly = true;
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
            // clmNextPerformTime
            // 
            this.clmNextPerformTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmNextPerformTime.FillWeight = 80F;
            this.clmNextPerformTime.HeaderText = "Next Perform Time";
            this.clmNextPerformTime.MinimumWidth = 8;
            this.clmNextPerformTime.Name = "clmNextPerformTime";
            this.clmNextPerformTime.ReadOnly = true;
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
            // ctxScript
            // 
            this.ctxScript.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctxScript.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdEdit,
            this.cmdToggleDisable,
            this.cmdPerformNow,
            this.cmdDelete});
            this.ctxScript.Name = "ctxScript";
            this.ctxScript.Size = new System.Drawing.Size(202, 132);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(201, 32);
            this.cmdEdit.Text = "Edit";
            // 
            // cmdToggleDisable
            // 
            this.cmdToggleDisable.Name = "cmdToggleDisable";
            this.cmdToggleDisable.Size = new System.Drawing.Size(201, 32);
            this.cmdToggleDisable.Text = "Enable/Disable";
            // 
            // cmdPerformNow
            // 
            this.cmdPerformNow.Name = "cmdPerformNow";
            this.cmdPerformNow.Size = new System.Drawing.Size(201, 32);
            this.cmdPerformNow.Text = "Perform now";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(201, 32);
            this.cmdDelete.Text = "Delete";
            // 
            // ctlFilter
            // 
            this.ctlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlFilter.Location = new System.Drawing.Point(0, 0);
            this.ctlFilter.Margin = new System.Windows.Forms.Padding(0);
            this.ctlFilter.MaximumSize = new System.Drawing.Size(500, 40);
            this.ctlFilter.MinimumSize = new System.Drawing.Size(200, 40);
            this.ctlFilter.Name = "ctlFilter";
            this.ctlFilter.Size = new System.Drawing.Size(489, 40);
            this.ctlFilter.TabIndex = 2;
            // 
            // gtbRoot
            // 
            this.gtbRoot.Controls.Add(this.tlpRoot);
            this.gtbRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gtbRoot.Location = new System.Drawing.Point(1, 5);
            this.gtbRoot.Name = "gtbRoot";
            this.gtbRoot.Size = new System.Drawing.Size(984, 477);
            this.gtbRoot.TabIndex = 1;
            this.gtbRoot.TabStop = false;
            this.gtbRoot.Text = "Your backup Scripts";
            // 
            // CtlBackupScripts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gtbRoot);
            this.Name = "CtlBackupScripts";
            this.Padding = new System.Windows.Forms.Padding(1, 5, 1, 1);
            this.Size = new System.Drawing.Size(986, 483);
            this.tlpRoot.ResumeLayout(false);
            this.tlpButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ctxScript.ResumeLayout(false);
            this.gtbRoot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private DataGuardian.GUI.Controls.GuardianDataGridView dgvData;
        private System.Windows.Forms.FlowLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private CtlFilter ctlFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCurrentState;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLastPerform;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNextPerformTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLastState;
        private System.Windows.Forms.ContextMenuStrip ctxScript;
        private System.Windows.Forms.ToolStripMenuItem cmdEdit;
        private System.Windows.Forms.ToolStripMenuItem cmdToggleDisable;
        private System.Windows.Forms.ToolStripMenuItem cmdPerformNow;
        private System.Windows.Forms.ToolStripMenuItem cmdDelete;
        private System.Windows.Forms.GroupBox gtbRoot;
    }
}
