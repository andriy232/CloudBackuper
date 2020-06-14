using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NightKeeper.Helper.Backups
{
    public class RemoteBackupsState
    {
        public readonly List<RemoteBackup> Backups = new List<RemoteBackup>();
        public IStorageProvider StorageProvider { get; }

        public class RemoteBackup
        {
            public DateTime ModifiedDate { get; }
            public string BackupName { get; }
            public string UniqueId { get; }

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

        public RemoteBackupsState(IStorageProvider storageProvider,
            IEnumerable<(string id, string Name, DateTime ClientModified)> valueTuples)
        {
            StorageProvider = storageProvider;
            if (valueTuples != null)
                Backups.AddRange(valueTuples.Select(x => new RemoteBackup(x.id, x.Name, x.ClientModified)));
        }

        public override string ToString()
        {
            var sb = new StringBuilder(Backups.Count * 20);
            foreach (var b in Backups)
                sb.AppendLine(b.ToString());
            return $"{StorageProvider} - {sb}";
        }
    }
}