using System.Windows.Forms;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;

namespace DataGuardian.GUI.Controls
{
    public class FormBase : Form
    {
        protected readonly ICore Core;

        public FormBase(ICore core)
        {
            Core = core;
        }

        public FormBase()
        {
            Core = CoreStatic.Instance;
        }
    }
}