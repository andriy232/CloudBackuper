using System.Windows.Forms;

namespace DataGuardian.GUI.Windows
{
    public partial class WndConfirmation : Form
    {
        public WndConfirmation(string message)
        {
            InitializeComponent();

            lblMessage.Text = message;
        }
    }
}
