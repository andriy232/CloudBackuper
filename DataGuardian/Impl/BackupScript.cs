using DataGuardian.Plugins;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DataGuardian.Impl
{
    [Table("BackupScripts")]
    public class BackupScript : IBackupScript
    {
        [Column("CreateDate")] public DateTime CreateTimestamp { get; set; }

        [Column("Enabled")] public bool Enabled { get; set; } = true;

        [Column("Name")] public string Name { get; set; }

        [Column("TargetPath")] public string TargetPath { get; set; }

        [Key, Column("Id")] public int Id { get; set; }

        [Column("Steps")] public string StepsState { get; set; }

        public DateTime LastPerformTime =>
            Steps != null && Steps.Any() ? Steps.Max(x => x.LastPerformTime) : DateTime.MinValue;

        public BackupResultState LastPerformState =>
            Steps != null && Steps.Any(x => !string.IsNullOrWhiteSpace(x.LastState))
                ? BackupResultState.Failed
                : BackupResultState.Success;

        private List<IBackupStep> _scriptStateCache;

        private BackupCurrentState _currentState;

        public List<IBackupStep> Steps
        {
            get => _scriptStateCache ??= DeserializeSteps();
            set => StepsState = SerializeSteps(_scriptStateCache = value);
        }

        [NotMapped]
        public BackupCurrentState CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;
                    CurrentStateChanged?.Invoke(this, _currentState);
                }
            }
        }

        public event EventHandler<BackupCurrentState> CurrentStateChanged;
        public void SetSerializedState()
        {
            StepsState = SerializeSteps(_scriptStateCache);
        }

        [NotMapped]
        public DateTime NextPerformTime =>
            Steps != null && Steps.Any() ? Steps.Min(x => x.NextPerformDate) : DateTime.MinValue;

        private static string SerializeSteps(IReadOnlyCollection<IBackupStep> value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }

        private List<IBackupStep> DeserializeSteps()
        {
            if (string.IsNullOrWhiteSpace(StepsState))
                return new List<IBackupStep>();

            return JsonConvert.DeserializeObject<List<BackupStep>>(StepsState).Cast<IBackupStep>().ToList();
        }

        public BackupScript()
        {
            CreateTimestamp = DateTime.Now;
        }

        public BackupScript(string name, string targetPath, string backupFileName,
            IEnumerable<IBackupStep> steps) : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));
            if (string.IsNullOrWhiteSpace(targetPath))
                throw new ArgumentException(nameof(targetPath));
            if (string.IsNullOrWhiteSpace(backupFileName))
                throw new ArgumentException(nameof(backupFileName));

            Name = name;
            TargetPath = targetPath;
            Steps = steps.ToList();
        }

        public override string ToString()
        {
            return $"Script: {Name}, {TargetPath}";
        }
    }
}