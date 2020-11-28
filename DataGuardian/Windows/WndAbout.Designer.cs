
namespace DataGuardian.Windows
{
    partial class WndAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WndAbout));
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.tlpRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 2;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.875F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.125F));
            this.tlpRoot.Controls.Add(this.pictureBox1, 0, 0);
            this.tlpRoot.Controls.Add(this.label1, 1, 0);
            this.tlpRoot.Controls.Add(this.label3, 1, 1);
            this.tlpRoot.Controls.Add(this.label4, 1, 2);
            this.tlpRoot.Controls.Add(this.label2, 1, 3);
            this.tlpRoot.Controls.Add(this.linkLabel1, 1, 4);
            this.tlpRoot.Controls.Add(this.label5, 1, 5);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 0);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 6;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 206F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpRoot.Size = new System.Drawing.Size(798, 444);
            this.tlpRoot.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::DataGuardian.Properties.Resources.Img_Logo;
            this.pictureBox1.Location = new System.Drawing.Point(15, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(15);
            this.pictureBox1.Name = "pictureBox1";
            this.tlpRoot.SetRowSpan(this.pictureBox1, 3);
            this.pictureBox1.Size = new System.Drawing.Size(248, 310);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label1.Location = new System.Drawing.Point(281, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(514, 58);
            this.label1.TabIndex = 1;
            this.label1.Text = "DataGuardian";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(281, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(514, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Project for Master degree";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(281, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(514, 50);
            this.label4.TabIndex = 5;
            this.label4.Text = "Powerful tool for creating and uploading backups \r\nof data into your cloud storag" +
    "e";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 349);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(514, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "by Harasivka Andrii";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(281, 388);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(514, 20);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "andriygarasivka@gmail.com";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(281, 420);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(514, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Ukraine, Ternopil. 2020";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WndAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 444);
            this.Controls.Add(this.tlpRoot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(820, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(820, 500);
            this.Name = "WndAbout";
            this.Text = "About Application";
            this.tlpRoot.ResumeLayout(false);
            this.tlpRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label5;
    }
}