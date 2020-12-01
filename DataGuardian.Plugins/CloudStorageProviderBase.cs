using System;
using System.Threading.Tasks;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Plugins
{
    public abstract class CloudStorageProviderBase<T> : PluginBase, ICloudStorageProvider where T : SettingsBase
    {
        public virtual string Name { get; }
        public virtual string Description { get; }
        public virtual byte[] Logo { get; }
        public virtual Guid Id { get; }

        protected T GetSettings(IAccount account)
        {
            if (account == null)
                return null;
            return Core.CloudAccountsManager.GetSettings<T>(account);
        }

        public abstract object TryAuth();
        public abstract Task<RemoteBackupsState> GetBackupState(IAccount account, string backupFileName);
        public abstract Task UploadBackupAsync(IAccount account, LocalArchivedBackup localBackup);
        public abstract Task DownloadBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup, string outputPath);
        public abstract Task DeleteBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup);
    }

    public class SettingsBase
    {
    }
}