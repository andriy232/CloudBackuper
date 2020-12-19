using System;
using Newtonsoft.Json.Linq;

namespace DataGuardian.Plugins
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }

        public string Source { get; set; } = "Default";

        public string RenderedMessage { get; set; }

        public string Exception { get; set; }

        public InfoLogLevel Level { get; set; }

        public string Properties { get; set; }

        public string Message =>
            string.IsNullOrWhiteSpace(Exception)
                ? RenderedMessage.Replace("{Message}", SavedMessage).Replace("in {Source}", string.Empty)
                : $"{RenderedMessage.Replace("{Message}", SavedMessage).Replace("in {Source}", string.Empty)}: {Exception}";

        public string SavedSource => string.IsNullOrWhiteSpace(Properties)
            ? Source
            : JObject.Parse(Properties).GetValue("Source")?.Value<string>();

        private string SavedMessage => string.IsNullOrWhiteSpace(Properties)
            ? null
            : JObject.Parse(Properties).GetValue("Message")?.Value<string>();

        // ReSharper disable once UnusedMember.Global
        public LogEntry()
        {
            // for Dapper
        }

        public LogEntry(InfoLogLevel level, string source, string message, Exception exception = null)
        {
            Level = level;
            Timestamp = DateTime.Now;
            Source = source;
            RenderedMessage = message ?? throw new ArgumentException(nameof(message));
            Exception = exception?.ToString();
        }

        public override string ToString()
        {
            return $"[{Timestamp}] [{Level.ToString().PadRight(5)}] {Source} : {Message}";
        }
    }
}