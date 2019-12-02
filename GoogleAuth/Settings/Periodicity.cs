using System;
using System.Diagnostics;

namespace Helper.Settings
{
    public struct PeriodicitySettings
    {
        public static PeriodicitySettings Empty = new PeriodicitySettings(Periodicity.None, string.Empty);

        public enum Periodicity : byte
        {
            Every,
            DateTime,
            TimeSpan,
            None
        }

        private Periodicity Period { get; }
        private string Parameter { get; }

        public PeriodicitySettings(Periodicity period, string parameter)
        {
            Period = period;
            Parameter = parameter;
        }

        public override string ToString()
        {
            return $"{Period};{Parameter}";
        }

        public static PeriodicitySettings Parse(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var parts = value.Split(';');
                if (parts.Length == 2)
                {
                    if (Enum.TryParse<Periodicity>(parts[0], true, out var period))
                        return new PeriodicitySettings(period, parts[1]);
                }
            }

            Debug.Assert(false, "value is empty or have incorrect format");
            return Empty;
        }
    }
}