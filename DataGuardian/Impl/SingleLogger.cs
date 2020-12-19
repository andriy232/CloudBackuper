using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Dapper;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using Serilog;

namespace DataGuardian.Impl
{
    public class SingleLogger : PluginBase, ISingleLogger
    {
        public event EventHandler<NewLogEventArgs> NewLog;
        private string _logFilePath;
        private string _logFileDir;

        public override string Name => "Logger";

        public override void Init(ICore core)
        {
            base.Init(core);

            _logFileDir = Path.Combine(core.Settings.DataDirectory, "Logs");
            _logFilePath = Path.Combine(_logFileDir, "DataGuardian_log.sqlite");

            var directory = Path.GetDirectoryName(_logFilePath);
            FileSystem.CreateDirectoryIfNotExists(directory);

            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.SQLite(_logFilePath)
                .CreateLogger();

            CurrentLog = Serilog.Log.Logger;
        }

        public IEnumerable<LogEntry> ReadLogs()
        {
            try
            {
                if (!File.Exists(_logFilePath))
                    return Enumerable.Empty<LogEntry>();

                using var connection = new SQLiteConnection($"Data Source={_logFilePath}; Version=3; Read Only=True;");
                return connection.Query<LogEntry>("select * from Logs");
            }
            catch (Exception ex)
            {
                Log(ex);
                return Enumerable.Empty<LogEntry>();
            }
        }

        private ILogger CurrentLog { get; set; }

        public void Log(string message)
        {
            Log("Default", message);
        }

        public void Log(Exception ex)
        {
            Log($"An error occured -> {ex}");
        }

        public void Log(string source, string message)
        {
            Log(InfoLogLevel.Information, source, message);
        }

        public void Log(string source, Exception ex)
        {
            Log(InfoLogLevel.Error, source, string.Empty, ex);
        }

        public void Log(InfoLogLevel level, string source, string message, Exception ex = null)
        {
            if (ex == null)
            {
                switch (level)
                {
                    case InfoLogLevel.Error:
                        Serilog.Log.Error("{Message} in {Source}", message, source);
                        break;
                    case InfoLogLevel.Information:
                        Serilog.Log.Information("{Message} in {Source}", message, source);
                        break;
                    case InfoLogLevel.Warning:
                        Serilog.Log.Warning("{Message} in {Source}", message, source);
                        break;
                    case InfoLogLevel.Debug:
                        Serilog.Log.Debug("{Message} in {Source}", message, source);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(level), level, null);
                }
            }
            else
                Serilog.Log.Error(ex, "Error in {Source}", source);

            FireNewLog(new LogEntry(level, source, message, ex));
        }

        private void FireNewLog(LogEntry logEntry)
        {
            NewLog?.Invoke(this, new NewLogEventArgs(logEntry));
        }
    }
}