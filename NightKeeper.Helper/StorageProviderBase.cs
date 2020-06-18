using NightKeeper.Helper.Backups;
using NightKeeper.Helper.Settings;
using System;
using System.Threading.Tasks;

namespace NightKeeper.Helper
{
    public abstract class StorageProviderBase<T> : IStorageProvider where T : SettingsBase
    {
        protected Core.Core Core;
        private ConnectionStatus _connectionStatus;

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

        public abstract object TryAuth();

        public bool IsConnected()
        {
            return ConnectionStatus == ConnectionStatus.Connected;
        }

        public ConnectionStatus ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                if (_connectionStatus == value)
                    return;

                _connectionStatus = value;
                ConnectionStatusChanged?.Invoke(this, value);
            }
        }

        public EventHandler<ConnectionStatus> ConnectionStatusChanged { get; }

        public abstract Task<RemoteBackupsState> GetBackupState();

        public abstract Task UploadBackupAsync(LocalArchivedBackup localBackup);

        public abstract Task DownloadBackupAsync(RemoteBackupsState.RemoteBackup backup, string outputPath);

        public abstract Task DeleteBackupAsync(RemoteBackupsState.RemoteBackup backup);
    }
}