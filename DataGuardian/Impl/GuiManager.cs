using System;
using System.Windows.Forms;
using DataGuardian.Plugins;

namespace DataGuardian.Impl
{
    public class GuiManager : IGuiManager
    {
        public bool InvokeRequired => MainWindow != null && MainWindow.IsDisposed! && !MainWindow.Disposing &&
                                      MainWindow.InvokeRequired;

        public Form MainWindow { get; private set; }

        public void SetWindow(Form form)
        {
            MainWindow = form;
            MainWindow.Load += OnLoad;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            GuiLoaded?.Invoke(MainWindow, EventArgs.Empty);
        }

        public T Invoke<T>(Func<T> func)
        {
            try
            {
                return (T) MainWindow.Invoke(func);
            }
            catch
            {
                // ignored
                return default(T);
            }
        }

        public event EventHandler GuiLoaded;
    }
}