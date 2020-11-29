using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Core;

namespace DataGuardian.Impl
{
    [Table("BackupScripts")]
    public class BackupScript : IBackupScript
    {
        [Column("BackupFileName")] public string BackupFileName { get; set; }

        [Column("CreateTime")] public DateTime CreateTime { get; set; }

        [Column("Enabled")] public bool Enabled { get; }

        [Column("Name")] public string Name { get; set; }

        [Column("TargetPath")] public string TargetPath { get; set; }

        [Key, Column("Id")] public int Id { get; set; }

        public DateTime LastPerformTime => Steps.Any() ? Steps.Max(x => x.PerformTime) : DateTime.MinValue;

        public BackupScriptState LastPerformState => Steps.Any(x => x.LastState == BackupScriptState.Failed)
            ? BackupScriptState.Failed
            : BackupScriptState.Success;

        public List<IBackupStep> Steps { get; }

        public BackupScript()
        {
        }

        public BackupScript(string name, string targetPath, string backupFileName, IEnumerable<IBackupStep> steps)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));
            if (string.IsNullOrWhiteSpace(targetPath))
                throw new ArgumentException(nameof(targetPath));
            if (string.IsNullOrWhiteSpace(backupFileName))
                throw new ArgumentException(nameof(backupFileName));

            Name = name;
            TargetPath = targetPath;
            BackupFileName = backupFileName;
            Steps = steps.ToList();
        }

        public async Task DoBackup()
        {
            if (!FileSystem.Exists(TargetPath))
                throw new ApplicationException("Target path not exists");

            using var backup = new LocalArchivedBackup(TargetPath, BackupFileName);
            //       await CloudProviderAccount.UploadBackup(backup);
        }

        public async Task DoRestore(RemoteBackupsState.RemoteBackup lastBackup)
        {
            if (FileSystem.Exists(TargetPath))
                throw new ApplicationException("Target path already exists");

            var zipPath = FileSystem.GetTempFilePath(".zip");
            // await CloudProviderAccount.CloudStorageProvider.DownloadBackupAsync(lastBackup, zipPath);

            await Zipper.Unzip(zipPath, TargetPath);
        }

        public override string ToString()
        {
            return $"Script: {Name}, {TargetPath}";
        }
    }
}