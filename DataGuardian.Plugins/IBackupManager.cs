using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGuardian.Plugins
{
    public interface IBackupManager : IPlugin
    {
        void EditBackupScriptGui(IBackupScript backupScript);
        void CreateBackupScriptGui();

        void EditBackupScript(IBackupScript backupScript, IBackupScript newBackupScript);
        void RemoveBackupScript(IBackupScript backupScript);
        void AddNewBackupScript(IBackupScript backupScript);
        Task Perform(IBackupScript script);

        event EventHandler<IEnumerable<IBackupScript>> BackupScriptsChanged;

        IEnumerable<IBackupScript> BackupScripts { get; }
        
    }
}