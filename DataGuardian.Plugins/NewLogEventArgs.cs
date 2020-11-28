using System;

namespace DataGuardian.Plugins
{
    public class NewLogEventArgs : EventArgs
    {
        public LogEntry Log { get; }

        public NewLogEventArgs(LogEntry log)
        {
            Log = log;
        }
    }
}