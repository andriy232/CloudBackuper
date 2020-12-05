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

        public RemoteBackupsState(
            ICloudStorageProvider cloudStorageProvider,
            IEnumerable<(string id, string Name, DateTime ClientModified)> valueTuples)
        {
            CloudStorageProvider = cloudStorageProvider;

            if (valueTuples != null)
                Backups.AddRange(valueTuples.Select(x => new RemoteBackup(x.id, x.Name, x.ClientModified)));
        }

        public RemoteBackupsState(
            ICloudStorageProvider cloudStorageProvider,
            IEnumerable<RemoteBackup> backups)
        {
            CloudStorageProvider = cloudStorageProvider;

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