using DataGuardian.Controls;
using DataGuardian.Plugins.Plugins;
using System;
using System.Collections.Generic;

namespace DataGuardian.Plugins
{
    public interface IBackupScript : IDbModel
    {
        DateTime CreateTime { get; }

        bool Enabled { get; }

        DateTime LastPerformTime { get; }

        BackupScriptState LastPerformState { get; }

        string Name { get; }

        string TargetPath { get; }

        List<IBackupStep> Steps { get; }
    }

    public interface IBackupStep : IDbModel, ICloneable
    {
        ICloudProviderAccount Account { get; }

        string TargetPath { get; }
        
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
    }

    public enum BackupScriptState
    {
        Success,
        Failed
    }

    public interface IDbModel
    {
        int Id { get; }
    }
}