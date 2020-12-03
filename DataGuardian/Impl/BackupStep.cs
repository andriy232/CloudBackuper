using DataGuardian.Controls;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Core;
using DataGuardian.Plugins.Plugins;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataGuardian.Impl
{
    public class BackupStep : IBackupStep
    {
        public string TargetPath { get; set; }

        public string BackupFileName { get; set; }

        public BackupAction Action { get; set; } = BackupAction.BackupTo;

        public string ActionParameter { get; set; }

        public BackupPeriod Period { get; set; } = BackupPeriod.Daily;

        public int RecurEvery { get; set; } = 1;

        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(1);

        public List<string> PeriodParametersList { get; set; }

        public string LastState { get; set; }

        public int AccountId { get; set; }

        public DateTime LastPerformTime { get; set; }

        [JsonIgnore]
        public IEnumerable<string> PeriodParameters
        {
            get => PeriodParametersList ?? Enumerable.Empty<string>();
            set => PeriodParametersList = value.ToList();
        }

        public DateTime Min(DateTime dt1, DateTime dt2)
        {
            return dt1.Ticks > dt2.Ticks ? dt1 : dt2;
        }

        [JsonIgnore]
        public DateTime NextPerformDate
        {
            get
            {
                var nextPerformDate = Min(LastPerformTime, StartDate);

                switch (Period)
                {
                    case BackupPeriod.OneTime:
                        if (LastPerformTime != DateTime.MinValue) 
                            return DateTime.MaxValue;

                        return StartDate;

                    case BackupPeriod.Daily:
                        if (LastPerformTime == DateTime.MinValue)
                            return StartDate;
                        return LastPerformTime.AddDays(RecurEvery);

                    case BackupPeriod.Weekly:
                        var daysOfWeek = PeriodParameters.Select(x =>
                                Enum.TryParse<DayOfWeek>(x, true, out var result) ? result : DayOfWeek.Monday)
                            .Distinct()
                            .ToList();
                        
                        if (daysOfWeek.Count != 0)
                        {
                            while (!daysOfWeek.Contains(nextPerformDate.DayOfWeek))
                                nextPerformDate = nextPerformDate.AddDays(1);
                        }
                        else
                        {
                            nextPerformDate = nextPerformDate.AddDays(7 * RecurEvery);
                        }

                        return nextPerformDate;

                    case BackupPeriod.Monthly:
                        var mothtes = PeriodParameters.Select(x =>
                                Enum.TryParse<Month>(x, true, out var result) ? result : Month.January)
                            .Distinct()
                            .ToList();

                        if (mothtes.Count > 0)
                        {
                            do
                            {
                                var daysInMonth = CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(
                                    nextPerformDate.Year, nextPerformDate.Month);

                                nextPerformDate = nextPerformDate.AddDays(daysInMonth);
                            } while (!mothtes.Contains((Month) nextPerformDate.Month));
                        }

                        return nextPerformDate;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        [JsonIgnore]
        public IAccount Account
        {
            get => CoreStatic.Instance.CloudAccountsManager.Accounts.FirstOrDefault(x => x.Id == AccountId);
            set => AccountId = value.Id;
        }

        public async Task Perform()
        {
            Exception runTimeException = null;
            try
            {

                switch (Action)
                {
                    case BackupAction.BackupTo:
                        await PerformBackup();
                        break;
                    case BackupAction.RestoreTo:
                        await PerformRestore();
                        break;
                    case BackupAction.CopyTo:
                        PerformCopy();
                        break;
                    case BackupAction.SendToEmail:
                        PerformSendToEmail();
                        break;
                    case BackupAction.Archive:
                        PerformToArchive();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                runTimeException = ex;
            }
            finally
            {
                LastPerformTime = DateTime.Now;
                LastState = runTimeException?.ToString() ?? string.Empty;
            }
        }

        public bool CheckIfLocalCopyExists()
        {
            var counter = 0;
            while (true)
            {
                try
                {
                    return FileSystem.Exists(TargetPath);
                }
                catch (Exception ex)
                {
                    if (counter++ > 10)
                        return false;
                    Thread.Sleep(1000);
                }
            }
        }

        private async Task PerformRestore()
        {
            if (Account == null)
                throw new ArgumentException(nameof(Account));
            if (Account?.CloudStorageProvider == null)
                throw new ArgumentException(nameof(Account.CloudStorageProvider));
            if (string.IsNullOrWhiteSpace(BackupFileName))
                throw new ArgumentException(nameof(BackupFileName));

            var latestBackups = await Account.CloudStorageProvider.GetBackupState(Account, BackupFileName);

            var lastBackup = latestBackups.Backups.LastOrDefault();
            if (lastBackup == null)
                throw new ApplicationException("no backups");

            var zipPath = FileSystem.GetTempFilePath(".zip");
            await Account.CloudStorageProvider.DownloadBackupAsync(Account, lastBackup, zipPath);

            await Zipper.Unzip(zipPath, TargetPath);
        }

        private void PerformToArchive()
        {
            if (!FileSystem.IsValidPath(TargetPath))
                throw new ArgumentException(nameof(TargetPath));

            if (!FileSystem.IsValidPath(ActionParameter))
                throw new ArgumentException(nameof(ActionParameter));

            Zipper.CreateZip(TargetPath, ActionParameter);
        }

        private void PerformSendToEmail()
        {
            if (!FileSystem.IsValidPath(TargetPath))
                throw new ArgumentException(nameof(TargetPath));
        }

        private void PerformCopy()
        {
            if (!FileSystem.IsValidPath(TargetPath))
                throw new ArgumentException(nameof(TargetPath));

            if (!FileSystem.IsValidPath(ActionParameter))
                throw new ArgumentException(nameof(ActionParameter));

            FileSystem.Copy(TargetPath, ActionParameter);
        }

        private async Task PerformBackup()
        {
            if (Account == null)
                throw new ArgumentException(nameof(Account));
            if (Account?.CloudStorageProvider == null)
                throw new ArgumentException(nameof(Account.CloudStorageProvider));
            if (!FileSystem.IsValidPath(TargetPath))
                throw new ArgumentException(nameof(TargetPath));

            var newLocalBackup = new LocalArchivedBackup(TargetPath, BackupFileName);
            await Account.CloudStorageProvider.UploadBackupAsync(Account, newLocalBackup);

            CoreStatic.Instance.Logger.Log(InfoLogLevel.Info, nameof(PerformBackup), "Backup successfully uploaded");
        }

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
                PeriodParametersList = PeriodParametersList
            };
        }
    }
}