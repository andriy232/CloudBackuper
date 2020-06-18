using NightKeeper.Helper.Backups;
using System.Threading.Tasks;

namespace NightKeeper.Helper
{
    public interface IConnection
    {
        IStorageProvider StorageProvider { get; }

        int Id { get; }

        byte[] Logo { get; }

        Task<RemoteBackupsState> GetRemoteBackups();
        Task Upload(LocalArchivedBackup localBackup);
    }
}