using System;
using System.Runtime.ExceptionServices;

namespace DataGuardian.Plugins.Core
{
    internal sealed class UnhandledExceptionHandler
    {
        private ICore _core;

        public void Init(ICore core)
        {
            _core = core;
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
            _core.Logger.Log(e);
        }
    }
}