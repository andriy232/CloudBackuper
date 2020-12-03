using System;
using System.Diagnostics;
using System.Windows.Forms;
using DataGuardian.Plugins.Core;
using DataGuardian.Windows;

namespace DataGuardian.GUI
{
    public static class GuiHelper
    {
        public static void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public static void ShowMessage(Exception ex)
        {
            ShowMessage(ex.ToString());
        }

        public static void OpenDirectory(string path)
        {
            if (FileSystem.IsValidPath(path))
            {
                try
                {
                    Process.Start(path);
                }
                catch (Exception ex)
                {
                    ShowMessage(ex);
                }
            }
            else
            {
                ShowMessage("Not valid path");
            }
        }

        public static void StartProcess(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    Process.Start(path);
                }
                catch (Exception ex)
                {
                    ShowMessage(ex);
                }
            }
            else
            {
                ShowMessage("Not valid path");
            }
        }

        public static string ReadLine(string message)
        {
            string result;
            do
            {
                using var wnd = new WndEnterText(message);

                if (wnd.ShowDialog() == DialogResult.OK)
                    result = wnd.Result;
                else
                    throw new ApplicationException("canceled by user");
            } while (string.IsNullOrWhiteSpace(result));

            return result;
        }

        public static string ReadPath(string message)
        {
            string result = null;
            var f = CoreStatic.Instance.GuiManager.MainWindow;
            f.Invoke(((Action) (() =>
            {
                do
                {
                    using var wnd = new WndEnterPath(message);

                    if (wnd.ShowDialog() == DialogResult.OK)
                        result = wnd.Params.TargetPath;
                    else
                        throw new ApplicationException("canceled by user");
                } while (string.IsNullOrWhiteSpace(result));

            })), null);

            return result;
        }

        public static DialogResult ShowConfirmationDialog(string message)
        {
            var result = DialogResult.None;
            var f = CoreStatic.Instance.GuiManager.MainWindow;
            
            f.Invoke(((Action) (() =>
            {
                using (var wnd = new WndConfirmation(message))
                    result = wnd.ShowDialog();

            })), null);

            return result;
        }
    }
}