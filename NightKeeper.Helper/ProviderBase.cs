using System;
using System.Threading.Tasks;
using NightKeeper.Helper.Backups;
using NightKeeper.Helper.Settings;

namespace NightKeeper.Helper
{
    public abstract class ProviderBase<T> : IProvider where T : SettingsBase
    {
        protected Core.Core Core;

        public abstract Guid Id { get; }
        public abstract string Name { get; }
        public abstract byte[] Logo { get; }

        public void Init(Core.Core core)
        {
            Core = core;
        }

        protected T GetSettings()
        {
            return Core.Settings.ReadSettings<T>(Id);
        }

        protected void SaveSettings(T settings)
        {
            Core.Settings.SaveSettings(Id, settings);
        }

        public abstract object GetConnectionValues();

        public abstract Task<RemoteBackupsState> GetRemoteBackups();

        public abstract Task Upload(LocalBackup localBackup);

        public abstract Task DownloadAsync(RemoteBackupsState.RemoteBackup backup, string outputPath);

        public abstract Task DeleteAsync(RemoteBackupsState.RemoteBackup backup);
    }
}