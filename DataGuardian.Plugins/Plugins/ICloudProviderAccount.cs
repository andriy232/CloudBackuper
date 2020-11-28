using System.Threading.Tasks;
using DataGuardian.Plugins.Backups;

namespace DataGuardian.Plugins.Plugins
{
    public interface ICloudProviderAccount: IDbModel
    {
        ICloudStorageProvider CloudStorageProvider { get; }

        string Name { get; }
        string ConnectionSettings { get; }

        Task<RemoteBackupsState> GetRemoteBackups();
        Task UploadBackup(LocalArchivedBackup localBackup);
    }
}