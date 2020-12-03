using System;
using System.Windows.Forms;

namespace DataGuardian.Controls
{
    public partial class CtlFilter : UserControl
    {
        public event EventHandler<string> FilterChanged;

        public CtlFilter()
        {
            InitializeComponent();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            FilterChanged?.Invoke(this, txtFilter.Text?.Trim());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFilter.Text = string.Empty;
        }
    }
}
