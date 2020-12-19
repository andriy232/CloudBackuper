using System.Windows.Forms;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;

namespace DataGuardian.GUI.Controls
{
    public class UserControlBase : UserControl
    {
        protected ICore Core => CoreStatic.Instance;
    }
}