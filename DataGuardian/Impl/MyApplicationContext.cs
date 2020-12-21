using System;
using System.Windows.Forms;
using DataGuardian.Plugins.Core;
using DataGuardian.Properties;
using DataGuardian.Windows;

namespace DataGuardian.Impl
{
    internal class MyApplicationContext : ApplicationContext
    {
        private static Form ConfigWindow => CoreStatic.Instance.GuiManager.MainWindow;

        public MyApplicationContext()
        {
            var configMenuItem = new MenuItem("Show", ShowWindow);
            var exitMenuItem = new MenuItem("Exit", Exit);

            var notifyIcon = new NotifyIcon
            {
                Icon = Resources.ApplicationIcon,
                ContextMenu = new ContextMenu(new[] {configMenuItem, exitMenuItem}),
                Visible = true
            };

            notifyIcon.DoubleClick += OnNotifyIconDoubleClick;
         
            Application.Run(new WndMain());
        }

        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            ShowMainWindow();
        }
        
        private void Exit(object sender, EventArgs e)
        {
            try
            {
                ConfigWindow.Dispose();
            }
            catch
            {
                // ignored
            }

            try
            {
                Application.Exit();
            }
            catch
            {
                // ignored
            }
        }

        private void ShowWindow(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            if (ConfigWindow.Visible)
            {
                ConfigWindow.Activate();
            }
            else
            {
                ConfigWindow.Show();
            }
        }
    }
}