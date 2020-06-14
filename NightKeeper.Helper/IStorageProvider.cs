using System;
using System.Threading.Tasks;
using NightKeeper.Helper.Backups;

namespace NightKeeper.Helper
{
    public interface IStorageProvider
    {
        Guid Id { get; }

        string Name { get; }

        byte[] Logo { get; }

        ConnectionStatus ConnectionStatus { get; }

        void Init(Core.Core core);

        object GetConnectionValues();

        bool IsConnected();

        EventHandler<ConnectionStatus> ConnectionStatusChanged { get; }

        Task<RemoteBackupsState> GetBackups();

        Task UploadBackupAsync(LocalBackup localBackup);

        Task DownloadBackupAsync(RemoteBackupsState.RemoteBackup backup, string outputPath);

        Task DeleteBackupAsync(RemoteBackupsState.RemoteBackup backup);
    }
}