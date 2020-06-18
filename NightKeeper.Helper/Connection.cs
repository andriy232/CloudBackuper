using NightKeeper.Helper.Backups;
using System.Threading.Tasks;

namespace NightKeeper.Helper
{
    public class Connection : IConnection
    {
        public IStorageProvider StorageProvider { get; set; }

        public object ConnectionSettings { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public byte[] Logo => StorageProvider?.Logo;

        public Task<RemoteBackupsState> GetRemoteBackups()
        {
            return StorageProvider.GetBackupState();
        }

        public Task Upload(LocalArchivedBackup localBackup)
        {
            return StorageProvider.UploadBackupAsync(localBackup);
        }

        public Connection()
        {
        }

        public Connection(int id, string name, IStorageProvider storageProvider, object connectionSettings)
        {
            Id = id;
            StorageProvider = storageProvider;
            ConnectionSettings = connectionSettings;
            Name = name;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Connection conn)
                return IsSame(this, conn);
            return false;
        }

        private bool IsSame(Connection conn1, Connection conn2)
        {
            return conn1?.StorageProvider?.Equals(conn2?.StorageProvider) ?? true &&
                   conn1.Name.Equals(conn2?.Name);
        }
    }
}