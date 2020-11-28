using DataGuardian.Controls;
using System;
using System.Linq;
using System.Windows.Forms;
using DataGuardian.GUI;
using DataGuardian.Impl;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;

namespace DataGuardian.Windows
{
    public partial class WndEnterPath : Form
    {
        public CreateScriptParameters Params
        {
            get => new CreateScriptParameters(txtName.Text, ctlPath.SelectedPath);
        }

        public WndEnterPath()
        {
            InitializeComponent();

            txtName.Text = $"New_backup_{Guid.NewGuid()}";
            ctlPath.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            txtName.Focus();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (!FileSystem.IsValidPath(ctlPath.SelectedPath))
            {
                GuiHelper.ShowMessage("Path not valid");
                return;
            }

            if (CoreStatic.Instance.BackupManager.BackupScripts.Any(x => x.Name == txtName.Text))
            {
                GuiHelper.ShowMessage("Name is not unique");
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
