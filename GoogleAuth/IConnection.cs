using System.Threading.Tasks;
using Helper.Backups;

namespace Helper
{
    public interface IConnection
    {
        IProvider Provider { get; }

        int Id { get; }

        Task<RemoteBackupsState> GetRemoteBackups();
        Task Upload(LocalBackup localBackup);
    }
}