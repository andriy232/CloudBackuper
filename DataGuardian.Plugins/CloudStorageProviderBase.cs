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

        protected T GetSettings(ICloudProviderAccount account)
        {
            return Core.CloudAccountsManager.GetSettings<T>(account);
        }

        public abstract object TryAuth();
        public abstract Task<RemoteBackupsState> GetBackupState();
        public abstract Task UploadBackupAsync(LocalArchivedBackup localBackup);
        public abstract Task DownloadBackupAsync(RemoteBackupsState.RemoteBackup backup, string outputPath);
        public abstract Task DeleteBackupAsync(RemoteBackupsState.RemoteBackup backup);
    }

    public class SettingsBase
    {
    }
}