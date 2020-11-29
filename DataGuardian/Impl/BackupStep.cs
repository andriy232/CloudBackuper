using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataGuardian.Controls;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Impl
{
    [Table("BackupSteps")]
    public class BackupStep : IBackupStep
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public ICloudProviderAccount Account { get; set; }
        public string TargetPath { get; set; }
        public BackupAction Action { get; set; } = BackupAction.BackupTo;
        public string ActionParameter { get; set; }
        public BackupPeriod Period { get; set; } = BackupPeriod.Daily;
        public int RecurEvery { get; set; } = 1;
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(1);
        public IEnumerable<string> PeriodParameters { get; set; }
        public BackupScriptState LastState { get; set; } = BackupScriptState.Success;
        public DateTime PerformTime { get; set; }

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