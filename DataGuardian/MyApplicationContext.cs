using System;
using System.Windows.Forms;
using DataGuardian.Properties;
using DataGuardian.Windows;

namespace DataGuardian
{
    internal class MyApplicationContext : ApplicationContext
    {
        private WndMain _configWindow;

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
        }

        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        private void Exit(object sender, EventArgs e)
        {
            try
            {
                _configWindow.Dispose();
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
            if (_configWindow == null || _configWindow.Disposing || _configWindow.IsDisposed)
                _configWindow = new WndMain();

            if (_configWindow.Visible)
            {
                _configWindow.Activate();
            }
            else
            {
                _configWindow.ShowDialog();
            }
        }
    }
}