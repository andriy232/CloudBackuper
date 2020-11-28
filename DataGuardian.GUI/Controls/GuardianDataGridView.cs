using System.Drawing;
using System.Windows.Forms;

namespace DataGuardian.GUI.Controls
{
    public class GuardianDataGridView : DataGridView
    {
        public GuardianDataGridView()
        {
            DefaultCellStyle.SelectionBackColor = Color.LightGray;
        }
    }
}