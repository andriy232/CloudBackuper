using System.Collections.Generic;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Plugins
{
    public interface ICore
    {
        ISettings Settings { get; }
        ISingleLogger Logger { get; }
        IBackupManager BackupManager { get; }
        ICloudAccountsManager CloudAccountsManager { get; }
        IEnumerable<ICloudStorageProvider> CloudStorageProviders { get; }
    }
}