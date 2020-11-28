using System;
using System.Collections.Generic;

namespace DataGuardian.Plugins
{
    public interface IBackupManager:IPlugin
    {
        void RemoveBackupScript(IBackupScript backupScript);
        void EditBackupScript(IBackupScript backupScript);
        void RequestNewBackupScriptDialog();

        event EventHandler<IEnumerable<IBackupScript>> BackupScriptsChanged;

        IEnumerable<IBackupScript> BackupScripts { get; }
    }

    public interface IPlugin
    {
        void Init(ICore core);
    }
}