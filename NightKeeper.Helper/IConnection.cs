using System.Threading.Tasks;
using NightKeeper.Helper.Backups;

namespace NightKeeper.Helper
{
    public interface IConnection
    {
        IProvider Provider { get; }

        int Id { get; }

        byte[] Logo { get; }

        Task<RemoteBackupsState> GetRemoteBackups();
        Task Upload(LocalBackup localBackup);
    }
}