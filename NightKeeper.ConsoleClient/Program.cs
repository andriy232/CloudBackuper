using NightKeeper.Helper.Core;
using NightKeeper.Helper.Settings;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NightKeeper.ConsoleClient
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                new Program().Go().GetAwaiter().GetResult();

                Core.Instance.Logger.Log("Program returned 0");
            }
            catch (Exception ex)
            {
                Core.Instance.Logger.Log(ex);
                Core.Instance.Logger.Log("Program returned -1");
                Console.ReadLine();
            }
        }

        private async Task Go()
        {
            var core = Core.Instance;

            foreach (var provider in core.Providers)
            {
                core.Logger.Log($"Available provider: {provider}");
            }

            foreach (var script in core.Scripts)
            {
                core.Logger.Log(script.ToString());
            }

            foreach (var provider in core.Providers)
            {
                if (core.Connections.All(x => x.StorageProvider != provider))
                {
                    var connectionSettings = provider.TryAuth();
                    core.AddConnection(
                        $"{provider.Name}-connection",
                        provider,
                        connectionSettings);
                }

                var connection = core.Connections.First(
                    x => x.StorageProvider.Name.Contains(provider.Name, StringComparison.OrdinalIgnoreCase));

                Debug.Assert(Directory.Exists("C:\\Users\\andri\\AppData\\Local\\Adobe"));

                var script = core.Scripts.FirstOrDefault(x => x.Name == $"Backup Adobe dir to {provider.Name}");
                if (script == null)
                {
                    core.AddScript($"Backup Adobe dir to {provider.Name}",
                        connection,
                        "C:\\Users\\andri\\AppData\\Local\\Adobe",
                        PeriodicitySettings.Manual);
                }

                script = core.Scripts.First(x => x.Name == $"Backup Adobe dir to {provider.Name}");
                try
                {
                    await script.DoBackup();
                }
                catch
                {
                    // ignored
                }

                var backupsState = await provider.GetBackupState();
                core.Logger.Log($"{connection}, {backupsState}");

                var backup = backupsState.Backups;
                Debug.Assert(backup.Count >= 1);

                core.FileSystem.Delete(script.TargetPath);
                var lastBackup = backup.OrderByDescending(x => x.ModifiedDate).First();
                await script.DoRestore(lastBackup);

                Debug.Assert(Directory.Exists("C:\\Users\\andri\\AppData\\Local\\Adobe"));

                await provider.DeleteBackupAsync(lastBackup);
                core.RemoveScript(script);
            }
        }
    }
}