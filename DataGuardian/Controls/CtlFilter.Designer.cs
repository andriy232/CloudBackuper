
namespace DataGuardian.Controls
{
    partial class CtlFilter
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
            this.lblFilter = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(3, 11);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(48, 20);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Filter:";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(57, 8);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(223, 26);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // CtlFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.lblFilter);
            this.MaximumSize = new System.Drawing.Size(5000, 40);
            this.MinimumSize = new System.Drawing.Size(200, 40);
            this.Name = "CtlFilter";
            this.Size = new System.Drawing.Size(283, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.TextBox txtFilter;
    }
}
