using System;
using System.Threading.Tasks;

namespace Helper.Settings
{
    public class Script
    {
        public PeriodicitySettings Periodicity { get; }
        public IConnection Connection { get; }
        public string TargetPath { get; }

        public string BackupFileName { get; set; }

        public Script(IConnection connection, string path, PeriodicitySettings periodicity)
        {
            Connection = connection ?? throw new ArgumentException(nameof(connection));
            TargetPath = path ?? throw new ArgumentException(nameof(path));
            Periodicity = periodicity;
            BackupFileName = "Backup";
        }

        public async Task Perform()
        {
            using (var backup = new Backup(TargetPath, BackupFileName))
            {
                await Connection.Provider.Upload(backup);
            }
        }

        public override string ToString()
        {
            return $"Script: {TargetPath}, {Connection}, {Periodicity}";
        }
    }
}