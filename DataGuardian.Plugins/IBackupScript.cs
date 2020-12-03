using DataGuardian.Controls;
using DataGuardian.Plugins.Plugins;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGuardian.Plugins
{
    public interface IBackupScript : IDbModel
    {
        DateTime CreateTimestamp { get; }

        bool Enabled { get; }

        DateTime LastPerformTime { get; }
        DateTime NextPerformTime { get; }

        BackupResultState LastPerformState { get; }

        string Name { get; }

        string TargetPath { get; }

        List<IBackupStep> Steps { get; }

        BackupCurrentState CurrentState { get; set; }

        event EventHandler<BackupCurrentState> CurrentStateChanged;
        void SetSerializedState();
    }

    public interface IBackupStep : ICloneable
    {
        IAccount Account { get; }

        string TargetPath { get; }

        string BackupFileName { get; }
        
        BackupAction Action { get; }

        string ActionParameter { get; }

        BackupPeriod Period { get; }

        int RecurEvery { get; }

        DateTime StartDate { get; }

        /// <summary>
        /// Days of week or monthes of year
        /// </summary>
        IEnumerable<string> PeriodParameters { get; }

        string LastState { get; }

        DateTime NextPerformDate { get; }

        DateTime LastPerformTime { get; }

        Task Perform();
        bool CheckIfLocalCopyExists();
    }

    public enum BackupResultState
    {
        Success,
        Failed
    }

    public enum BackupCurrentState
    {
        NotStarted,
        Processing,
        Finished,
    }

    public interface IDbModel
    {
        int Id { get; }
    }
}