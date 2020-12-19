using System;
using System.Runtime.ExceptionServices;

namespace DataGuardian.Plugins.Core
{
    public sealed class UnhandledExceptionHandler : PluginBase
    {
        public override string Name => "Unhandled Exceptions";

        public override void Init(ICore core)
        {
            base.Init(core);

            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            AppDomain.CurrentDomain.FirstChanceException += OnFirstChanceException;
        }

        private void OnFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            try
            {
                var ex = e.Exception;
                if (ex.Source == "EntityFramework"
                    || ex.Message.Contains("no such table: __MigrationHistory")
                    || ex.Message.Contains("no such table: EdmMetadata"))
                    return;

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
            Core.Logger.Log(e);
        }
    }
}