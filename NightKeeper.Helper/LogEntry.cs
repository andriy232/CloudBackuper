using System;

namespace NightKeeper.Helper
{
    public class LogEntry
    {
        public DateTime Time { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public InfoLogType Type { get; set; }

        public LogEntry(InfoLogType type, string source, string message)
        {
            Time = DateTime.Now;
            Source = source;
            Message = message ?? throw new ArgumentException(nameof(message));
        }

        public LogEntry()
        {
        }

        public override string ToString()
        {
            return $"[{Time}] [{Type.ToString().PadRight(5)}] {Source} : {Message}";
        }
    }
}