using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGuardian.Windows
{
    public partial class WndEnterText : Form
    {
        public string Result => txtResult.Text?.Trim();

        public string Question
        {
            get => label1.Text?.Trim();
            set => label1.Text = value?.Trim();
        }

        public WndEnterText(string question)
        {
            InitializeComponent();

            Question = string.IsNullOrWhiteSpace(question) ? throw new ArgumentException() : question;
            tlpRoot.SizeChanged += OnTableSizeChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SynchronizeSize();
        }

        private void OnTableSizeChanged(object sender, EventArgs e)
        {
            SynchronizeSize();
        }

        private void SynchronizeSize()
        {
            Size = new Size(tlpRoot.Width + tlpRoot.Margin.Horizontal,
                tlpRoot.Height + tlpRoot.Margin.Vertical);
        }
    }
}