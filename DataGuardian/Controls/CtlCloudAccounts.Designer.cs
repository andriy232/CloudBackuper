
namespace DataGuardian.Controls
{
    partial class CtlCloudAccounts
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvData = new DataGuardian.GUI.Controls.GuardianDataGridView();
            this.ctlFilter1 = new DataGuardian.Controls.CtlFilter();
            this.clmImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpRoot.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 2;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tlpRoot.Controls.Add(this.tlpButtons, 1, 0);
            this.tlpRoot.Controls.Add(this.dgvData, 0, 1);
            this.tlpRoot.Controls.Add(this.ctlFilter1, 0, 0);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 2;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Size = new System.Drawing.Size(374, 563);
            this.tlpRoot.TabIndex = 1;
            // 
            // tlpButtons
            // 
            this.tlpButtons.Controls.Add(this.btnCreate);
            this.tlpButtons.Controls.Add(this.btnDelete);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.tlpButtons.Location = new System.Drawing.Point(204, 0);
            this.tlpButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.Size = new System.Drawing.Size(170, 40);
            this.tlpButtons.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.BackgroundImage = global::DataGuardian.Properties.Resources.Img_Plus;
            this.btnCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCreate.Location = new System.Drawing.Point(92, 3);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 34);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackgroundImage = global::DataGuardian.Properties.Resources.Img_Close;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Location = new System.Drawing.Point(11, 3);
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
            this.clmImage,
            this.clmName,
            this.clmType});
            this.tlpRoot.SetColumnSpan(this.dgvData, 2);
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 43);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowHeadersWidth = 162;
            this.dgvData.RowTemplate.Height = 50;
            this.dgvData.Size = new System.Drawing.Size(368, 517);
            this.dgvData.TabIndex = 1;
            // 
            // ctlFilter1
            // 
            this.ctlFilter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlFilter1.Location = new System.Drawing.Point(0, 0);
            this.ctlFilter1.Margin = new System.Windows.Forms.Padding(0);
            this.ctlFilter1.MaximumSize = new System.Drawing.Size(500, 40);
            this.ctlFilter1.MinimumSize = new System.Drawing.Size(200, 40);
            this.ctlFilter1.Name = "ctlFilter1";
            this.ctlFilter1.Size = new System.Drawing.Size(204, 40);
            this.ctlFilter1.TabIndex = 2;
            // 
            // clmImage
            // 
            this.clmImage.FillWeight = 50F;
            this.clmImage.HeaderText = "Image";
            this.clmImage.MinimumWidth = 8;
            this.clmImage.Name = "clmImage";
            this.clmImage.ReadOnly = true;
            this.clmImage.Width = 70;
            // 
            // clmName
            // 
            this.clmName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmName.HeaderText = "Name";
            this.clmName.MinimumWidth = 8;
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
            // 
            // clmType
            // 
            this.clmType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmType.HeaderText = "Type";
            this.clmType.MinimumWidth = 8;
            this.clmType.Name = "clmType";
            this.clmType.ReadOnly = true;
            // 
            // CtlCloudAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpRoot);
            this.Name = "CtlCloudAccounts";
            this.Size = new System.Drawing.Size(374, 563);
            this.tlpRoot.ResumeLayout(false);
            this.tlpButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.FlowLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnDelete;
        private DataGuardian.GUI.Controls.GuardianDataGridView dgvData;
        private CtlFilter ctlFilter1;
        private System.Windows.Forms.DataGridViewImageColumn clmImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmType;
    }
}
