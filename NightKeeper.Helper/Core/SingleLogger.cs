using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;

namespace NightKeeper.Helper.Core
{
    public class SingleLogger
    {
        public event EventHandler<LogEntry> NewLog;
        public ILogger CurrentLog { get; set; }

        public void Log(string message)
        {
            NewLog?.Invoke(this, new LogEntry(LogEventLevel.Information, LogSources.Default, message));
            Serilog.Log.Warning(message);
        }

        public void Log(Exception ex)
        {
            NewLog?.Invoke(this, new LogEntry(LogEventLevel.Error, LogSources.Default, ex.ToString()));
            Serilog.Log.Error(ex, "Error");
        }

        public void Init()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Serilog.Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            CurrentLog = Serilog.Log.Logger;
        }
    }
}