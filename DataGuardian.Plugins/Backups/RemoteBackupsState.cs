using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Plugins.Backups
{
    public class RemoteBackupsState
    {
        public readonly List<RemoteBackup> Backups = new List<RemoteBackup>();
        public ICloudStorageProvider CloudStorageProvider { get; }
        public string BackupName { get; }
        public IBackupStep Parent { get; set; }

        public RemoteBackupsState(
            ICloudStorageProvider cloudStorageProvider,
            string backupFileName,
            IEnumerable<(string id, string Name, DateTime ClientModified)> valueTuples)
        {
            BackupName = backupFileName ?? throw new ArgumentException(nameof(backupFileName));
            CloudStorageProvider = cloudStorageProvider ?? throw new ArgumentException(nameof(cloudStorageProvider));

            if (valueTuples != null)
                Backups.AddRange(valueTuples.Select(x => new RemoteBackup(x.id, x.Name, x.ClientModified)));
        }

        public RemoteBackupsState(
            ICloudStorageProvider cloudStorageProvider,
            string backupFileName,
            IEnumerable<RemoteBackup> backups)
        {
            BackupName = backupFileName ?? throw new ArgumentException(nameof(backupFileName));
            CloudStorageProvider = cloudStorageProvider ?? throw new ArgumentException(nameof(cloudStorageProvider));

            if (backups != null)
                Backups.AddRange(backups);
        }

        public override string ToString()
        {
            var sb = new StringBuilder(Backups.Count * 20);
            foreach (var b in Backups)
                sb.AppendLine(b.ToString());
            return $"{CloudStorageProvider} - {sb}";
        }
    }

    public class RemoteBackup
    {
        public DateTime ModifiedDate { get; set; }
        public string BackupName { get; set; }
        public string UniqueId { get; set; }

        public RemoteBackup()
        {
        }

        public RemoteBackup(string id, string backupName, DateTime modifiedDate)
        {
            UniqueId = id;
            BackupName = backupName;
            ModifiedDate = modifiedDate;
        }

        public override string ToString()
        {
            return $"RemoteBackup: {BackupName} at {ModifiedDate}";
        }
    }
}