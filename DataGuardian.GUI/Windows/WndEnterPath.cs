using System;
using System.Linq;
using System.Windows.Forms;
using DataGuardian.Impl;
using DataGuardian.Plugins.Core;

namespace DataGuardian.GUI.Windows
{
    public partial class WndEnterPath : Form
    {
        public CreateScriptParameters Params => new CreateScriptParameters(txtName.Text, ctlPath.SelectedPath);

        public bool EnterNameVisible
        {
            get => txtName.Visible;
            set => txtName.Visible = value;
        }

        public WndEnterPath(string message, string name = "", string path = "")
        {
            InitializeComponent();

            lblTitle.Text = message;
            var index = message.IndexOf("https:");
            if (index > 0)
            {
                var end = message.LastIndexOf("/");
                lblTitle.LinkArea = new LinkArea(index, end-index);
            }

            txtName.Text = name;
            ctlPath.SelectedPath = path;

            EnterNameVisible = !string.IsNullOrWhiteSpace(name);

            if (string.IsNullOrWhiteSpace(path))
            {
                ctlPath.SetMode(FileSystemObject.File);
                ctlPath.SelectDialog(lblTitle.Text);
            }

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

    public enum FileSystemObject
    {
        File,
        Folder
    }
}