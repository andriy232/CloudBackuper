using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.LocalServer
{
    public class LocalServer : CloudStorageProviderBase<LocalServerSettings>
    {
        public override Guid Id => Guid.Parse("D67A4F3C-B930-4631-B690-E2EF53C93EE5");

        public override string Description => "DataGuardian Local server could be installed in your Network";

        public override byte[] Logo
        {
            get
            {
                using (var stream = new MemoryStream())
                {
                    Resources.Img_Server.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }

        public override string Name => "Local Server";

        public override object TryAuth()
        {
            throw new NotImplementedException();
        }

        public override Task<RemoteBackupsState> GetBackupState(IAccount account, string backupFileName)
        {
            throw new NotImplementedException();
        }

        public override Task UploadBackupAsync(IAccount account, LocalArchivedBackup localBackup)
        {
            throw new NotImplementedException();
        }

        public override Task DownloadBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup,
            string outputPath)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup)
        {
            throw new NotImplementedException();
        }
    }

    public class LocalServerSettings : SettingsBase
    {
    }
}