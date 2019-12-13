using System;
using System.Threading.Tasks;
using NightKeeper.Helper.Backups;

namespace NightKeeper.Helper
{
    public interface IProvider
    {
        Guid Id { get; }

        string Name { get; }

        byte[] Logo { get; }

        void Init(Core.Core core);

        object GetConnectionValues();

        Task<RemoteBackupsState> GetRemoteBackups();

        Task Upload(LocalBackup localBackup);

        Task DownloadAsync(RemoteBackupsState.RemoteBackup backup, string outputPath);

        Task DeleteAsync(RemoteBackupsState.RemoteBackup backup);
    }
}