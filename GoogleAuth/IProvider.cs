using System;
using System.Threading.Tasks;
using Helper.Backups;

namespace Helper
{
    public interface IProvider
    {
        Guid Id { get; }

        string Name { get; }

        object GetConnectionValues();

        Task<RemoteBackupsState> GetRemoteBackups();

        Task Upload(LocalBackup localBackup);

        Task DownloadAsync(RemoteBackupsState.RemoteBackup backup, string outputPath);

        Task DeleteAsync(RemoteBackupsState.RemoteBackup backup);
    }
}