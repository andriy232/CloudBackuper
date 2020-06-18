using NightKeeper.Helper.Backups;
using System;
using System.Threading.Tasks;

namespace NightKeeper.Helper
{
    public interface IStorageProvider
    {
        Guid Id { get; }

        string Name { get; }

        byte[] Logo { get; }

        ConnectionStatus ConnectionStatus { get; }

        void Init(Core.Core core);

        object TryAuth();

        bool IsConnected();

        EventHandler<ConnectionStatus> ConnectionStatusChanged { get; }

        Task<RemoteBackupsState> GetBackupState();

        Task UploadBackupAsync(LocalArchivedBackup localBackup);

        Task DownloadBackupAsync(RemoteBackupsState.RemoteBackup backup, string outputPath);

        Task DeleteBackupAsync(RemoteBackupsState.RemoteBackup backup);
    }
}