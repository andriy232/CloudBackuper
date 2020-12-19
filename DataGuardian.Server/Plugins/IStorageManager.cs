using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataGuardian.Plugins.Backups;

namespace DataGuardian.Server.Plugins
{
    public interface IStorageManager
    {
        Task SaveFileToFileSystem(string userDirectory, string fileName, Stream stream);

        Task<MemoryStream> ReadFile(string userDirectory, string backupId);

        IEnumerable<RemoteBackup> GetLocalBackupsState(string userDirectory, string backupName);

        Task Delete(string userDirectory, string backupId);
    }
}