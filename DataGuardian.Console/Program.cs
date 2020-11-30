using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGuardian.Controls;
using DataGuardian.Impl;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;

namespace DataGuardian.Console
{
    class Program
    {
        private static void Main()
        {
            try
            {
                new Program().Go().GetAwaiter().GetResult();

                CoreStatic.Instance.Logger.Log("Program returned 0");
            }
            catch (Exception ex)
            {
                CoreStatic.Instance.Logger.Log(ex);
                CoreStatic.Instance.Logger.Log("Program returned -1");
                System.Console.ReadLine();
            }
        }

        private async Task Go()
        {
            _ = new Core();
            var core = CoreStatic.Instance;
            var manager = core.BackupManager;

            foreach (var provider in core.CloudStorageProviders)
            {
                core.Logger.Log($"Available provider: {provider}");
            }

            TestBackupScripts(manager, core);
            //foreach (var provider in core.CloudStorageProviders)
            //{
            //    if (core.Connections.All(x => x.StorageProvider != provider))
            //    {
            //        var connectionSettings = provider.TryAuth();
            //        core.AddConnection(
            //            $"{provider.Name}-connection",
            //            provider,
            //            connectionSettings);
            //    }
            //
            //    var connection = core.Connections.First(
            //        x => x.StorageProvider.Name.Contains(provider.Name, StringComparison.OrdinalIgnoreCase));
            //
            //    Debug.Assert(Directory.Exists("C:\\Users\\andri\\AppData\\Local\\Adobe"));
            //
            //    var script = core.Scripts.FirstOrDefault(x => x.Name == $"Backup Adobe dir to {provider.Name}");
            //    if (script == null)
            //    {
            //        core.AddScript($"Backup Adobe dir to {provider.Name}",
            //            connection,
            //            "C:\\Users\\andri\\AppData\\Local\\Adobe",
            //            PeriodicitySettings.Manual);
            //    }
            //
            //    script = core.Scripts.First(x => x.Name == $"Backup Adobe dir to {provider.Name}");
            //
            //    await script.DoBackup();
            //
            //    var backupsState = await provider.GetBackupState();
            //    core.Logger.Log($"{connection}, {backupsState}");
            //
            //    var backup = backupsState.Backups;
            //    Debug.Assert(backup.Count >= 1);
            //
            //    core.FileSystem.Delete(script.TargetPath);
            //    var lastBackup = backup.OrderByDescending(x => x.ModifiedDate).First();
            //    await script.DoRestore(lastBackup);
            //
            //    Debug.Assert(Directory.Exists("C:\\Users\\andri\\AppData\\Local\\Adobe"));
            //
            //    //await provider.DeleteBackupAsync(lastBackup);
            //    //core.RemoveScript(script);
            //}
        }

        private static void TestBackupScripts(IBackupManager manager, ICore core)
        {
            foreach (var script in manager.BackupScripts)
            {
                core.Logger.Log(script.ToString());
            }

            var newBackupScript = (BackupScript) manager.BackupScripts.FirstOrDefault() ?? new BackupScript
            {
                Name = "my_new_backup_2",
                TargetPath = "C:\\Users\\Admin\\AppData\\Local\\AgenaTrader\\Cache",
                Steps = new List<IBackupStep>
                {
                    new BackupStep
                    {
                        Account = (CloudProviderAccount) core.CloudAccountsManager.Accounts.First(),
                        TargetPath = "C:\\Users\\Admin\\AppData\\Local\\AgenaTrader\\Cache",
                        BackupFileName = "Cache",
                        Action = BackupAction.BackupTo,
                        ActionParameter = core.CloudAccountsManager.Accounts.First().ToString(),
                        StartDate = DateTime.Today.AddDays(1),
                        RecurEvery = 2,
                        Period = BackupPeriod.Weekly,
                        PeriodParameters = new[] {DayOfWeek.Friday.ToString(), DayOfWeek.Saturday.ToString()},
                    }
                }
            };

            newBackupScript.Name = $"Backup_{DateTime.Now}";

            manager.EditBackupScript(newBackupScript, newBackupScript);

            var otherScripts = manager.BackupScripts.Except(new[] {newBackupScript}).ToList();
            foreach (var backupScript in otherScripts)
                manager.RemoveBackupScript(backupScript);

            System.Console.WriteLine(manager.BackupScripts.First().Name);
        }
    }
}