using System;
using System.Windows.Forms;
using DataGuardian.GUI;
using DataGuardian.Plugins;
using DataGuardian.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DataGuardian.Controls
{
    public partial class CtlPath : UserControl
    {
        private FileSystemObject _mode = FileSystemObject.Folder;

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
            SelectDialog();
        }

        public void SetMode(FileSystemObject file)
        {
            _mode = file;
        }

        public void SelectDialog()
        {
            try
            {
                switch (_mode)
                {
                    case FileSystemObject.File:
                        using (var fbd = new OpenFileDialog())
                        {
                            var result = fbd.ShowDialog();

                            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                                SelectedPath = fbd.FileName;
                            else
                                throw new ArgumentException("path not valid");
                        }

                        break;
                    case FileSystemObject.Folder:
                        using (var fbd = new FolderBrowserDialog())
                        {
                            var result = fbd.ShowDialog();

                            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                                SelectedPath = fbd.SelectedPath;
                            else
                                throw new ArgumentException("path not valid");
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                GuiHelper.ShowMessage(ex);
            }
        }
    }
}