using System;
using System.Threading.Tasks;
using DataGuardian.Plugins.Backups;

namespace DataGuardian.Plugins.Plugins
{
    public interface ICloudStorageProvider : IPlugin
    {
        Guid Id { get; }

        string Name { get; }

        public string Description { get; }

        byte[] Logo { get; }

        void Init(ICore core);

        object TryAuth();

        Task<RemoteBackupsState> GetBackupState(IAccount account, string backupFileName);

        Task UploadBackupAsync(IAccount account, LocalArchivedBackup localBackup);

        Task DownloadBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup, string outputPath);

        Task DeleteBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup);
    }
}