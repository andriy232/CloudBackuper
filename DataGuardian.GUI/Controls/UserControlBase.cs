using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using System.Windows.Forms;

namespace DataGuardian.GUI.UserControls
{
    public class UserControlBase : UserControl
    {
        protected ICore Core => CoreStatic.Instance;
    }
}
