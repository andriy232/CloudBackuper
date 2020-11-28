using System;

namespace DataGuardian.Plugins.Backups
{
    public interface ILocalBackup : IDisposable
    {
        string ResultPath { get; }
        void CreateArchive(string targetPath, string backupFileName);
    }
}