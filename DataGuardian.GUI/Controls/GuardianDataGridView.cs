using System.Drawing;
using System.Windows.Forms;

namespace DataGuardian.GUI.Controls
{
    public class GuardianDataGridView : DataGridView
    {
        public GuardianDataGridView()
        {
            DefaultCellStyle.SelectionBackColor = Color.LightGray;
            DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    var hitTestInfo = HitTest(e.X, e.Y);
                    ClearSelection();
                    if (hitTestInfo.RowIndex < 0)
                        return;

                    Rows[hitTestInfo.RowIndex].Selected = true;
                }
            }
            catch
            {
                // ignored
            }

            base.OnMouseDown(e);
        }
    }
}