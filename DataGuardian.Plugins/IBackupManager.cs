using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataGuardian.Plugins.Backups;

namespace DataGuardian.Plugins
{
    public interface IBackupManager : IPlugin
    {
        IEnumerable<IBackupScript> BackupScripts { get; }

        event EventHandler<IEnumerable<IBackupScript>> BackupScriptsChanged;

        void CreateBackupScriptGui();

        void EditBackupScriptGui(IBackupScript backupScript);
        void EditBackupScript(IBackupScript script, IBackupScript updatedScript);

        void RemoveBackupScriptGui(IBackupScript script);
        void RemoveBackupScript(IBackupScript script);

        Task DeleteRemoteBackup(IBackupStep step, RemoteBackup remoteBackup);

        Task ShowRemoteBackupsGui(IBackupScript script);
        Task ShowRemoteBackupsGui(IBackupStep step);

        Task Perform(IBackupScript script);
        
    }
}