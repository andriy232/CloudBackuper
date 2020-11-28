using System;
using System.Collections.Generic;

namespace DataGuardian.Plugins
{
    public interface ISingleLogger:IPlugin
    {
        void Log(string message);

        void Log(Exception ex);

        void Log(string source, Exception ex);

        event EventHandler<NewLogEventArgs> NewLog;

        IEnumerable<LogEntry> ReadLogs();
    }
}