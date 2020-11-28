using System;
using System.Windows.Forms;
using DataGuardian.GUI;
using DataGuardian.Plugins;

namespace DataGuardian.Controls
{
    public partial class CtlPath : UserControl
    {
        public string SelectedPath
        {
            get => txtPath.Text.Trim();
            set => txtPath.Text = value?.Trim();
        }

        public CtlPath()
        {
            InitializeComponent();
        }

        private void OnBtnOpenClick(object sender, EventArgs e)
        {
            GuiHelper.OpenDirectory(SelectedPath);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    var result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        SelectedPath = fbd.SelectedPath;
                    }
                    else
                    {
                        throw new ArgumentException("path not valid");
                    }
                }
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage(ex);
            }
        }
    }
}