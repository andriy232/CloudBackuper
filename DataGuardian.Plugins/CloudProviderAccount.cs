using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Plugins
{
    public class CloudProviderAccount : ICloudProviderAccount
    {
        [NotMapped]
        public ICloudStorageProvider CloudStorageProvider { get; set; }
        
        [Column("cloudId")]
        public Guid CloudStorageProviderId { get; set; }
        
        [Column("connectionSettings")]
        public string ConnectionSettings { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("id")]
        public int Id { get; set; }

        [NotMapped]
        public byte[] Logo => CloudStorageProvider?.Logo;

        public Task<RemoteBackupsState> GetRemoteBackups()
        {
            return CloudStorageProvider.GetBackupState();
        }

        public Task UploadBackup(LocalArchivedBackup localBackup)
        {
            return CloudStorageProvider.UploadBackupAsync(localBackup);
        }

        public CloudProviderAccount()
        {
        }

        public CloudProviderAccount(string name, ICloudStorageProvider cloudStorageProvider, string connectionSettings)
        {
            CloudStorageProvider = cloudStorageProvider;
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
            if (obj is CloudProviderAccount conn)
                return IsSame(this, conn);
            return false;
        }

        private bool IsSame(ICloudProviderAccount conn1, ICloudProviderAccount conn2)
        {
            return conn1.Id.Equals(conn2.Id);
        }
    }
}