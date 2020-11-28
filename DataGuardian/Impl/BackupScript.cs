using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGuardian.Controls;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Core;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Impl
{
    public class BackupScript : IBackupScript
    {
        public string BackupFileName { get; set; }
        public DateTime CreateTime { get; }
        public bool Enabled { get; }
        public DateTime LastPerformTime { get; }
        public BackupScriptState LastPerformState { get; }
        public string Name { get; set; }
        public string TargetPath { get; set; }
        public List<IBackupStep> Steps { get; }
        public int Id { get; }

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

    public class BackupStep : IBackupStep
    {
        public int Id { get; set; }
        public ICloudProviderAccount Account { get; set; }
        public string TargetPath { get; set; }
        public BackupAction Action { get; set; }
        public string ActionParameter { get; set; }
        public BackupPeriod Period { get; set; }
        public int RecurEvery { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<string> PeriodParameters { get; set; }
        public string LastState { get; set; } = "Not performed yet";

        public object Clone()
        {
            return new BackupStep
            {
                Account = Account,
                TargetPath = TargetPath,
                Action = Action,
                ActionParameter = ActionParameter,
                Period = Period,
                RecurEvery = RecurEvery,
                StartDate = StartDate,
                PeriodParameters = PeriodParameters
            };
        }
    }
}