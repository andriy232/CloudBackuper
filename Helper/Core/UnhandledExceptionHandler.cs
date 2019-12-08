using System;
using System.Runtime.ExceptionServices;

namespace Helper.Core
{
    internal sealed class UnhandledExceptionHandler
    {
        public void Init()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            AppDomain.CurrentDomain.FirstChanceException += OnFirstChanceException;
        }

        private void OnFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            try
            {
                var ex = e.Exception;
                HandleException(ex);
            }
            catch
            {
                // ignored
            }
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var ex = e.ExceptionObject as Exception;
                HandleException(ex);
            }
            catch
            {
                // ignored
            }
        }

        private void HandleException(Exception e)
        {
            Core.WriteLine(e);
        }
    }
}