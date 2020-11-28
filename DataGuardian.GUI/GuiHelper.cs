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
    }
}