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

        Task<RemoteBackupsState> GetBackupState();

        Task UploadBackupAsync(LocalArchivedBackup localBackup);

        Task DownloadBackupAsync(RemoteBackupsState.RemoteBackup backup, string outputPath);

        Task DeleteBackupAsync(RemoteBackupsState.RemoteBackup backup);
    }
}