using System;
using System.Linq;
using System.Threading.Tasks;
using NightKeeper.Helper;
using NightKeeper.Helper.Backups;
using NightKeeper.Helper.Core;
using NightKeeper.Helper.Settings;

namespace NightKeeper.ConsoleClient
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                new Program().Go().GetAwaiter().GetResult();

                Core.GetInstance().Log("Program returned 0");
            }
            catch (Exception ex)
            {
                Core.GetInstance().Log(ex);
                Core.GetInstance().Log("Program returned -1");
                Console.ReadLine();
            }
        }

        private async Task Go()
        {
            var core = Core.GetInstance();

            RemoteBackupsState remoteBackupsState = null;

            foreach (var provider in core.Providers)
            {
                core.Log($"Available provider: {provider}");
            }

            foreach (var connection in core.Connections)
            {
                remoteBackupsState = await connection.GetRemoteBackups();
                core.Log($"{connection}, {remoteBackupsState}");
            }

            foreach (var script in core.Scripts)
            {
                core.Log(script.ToString());
            }

            IConnection dropboxConnection;
            IConnection driveConnection;

            if (true)
            {
                var drive = core.Providers.First(x => x.Name.Contains("drive", StringComparison.OrdinalIgnoreCase));
                var driveValues = drive.GetConnectionValues();

                driveConnection = core.AddConnection(
                    "Google Drive",
                    drive,
                    driveValues);

                var dropbox = core.Providers.First(x => x.Name.Contains("dropbox", StringComparison.OrdinalIgnoreCase));
                var dropboxValues = dropbox.GetConnectionValues();

                dropboxConnection = core.AddConnection(
                    "Dropbox",
                    dropbox,
                    dropboxValues);
            }
            else
            {
                driveConnection =
                    core.Connections.First(x => x.Provider.Name.Contains("drive", StringComparison.OrdinalIgnoreCase));
                dropboxConnection =
                    core.Connections.First(x =>
                        x.Provider.Name.Contains("dropbox", StringComparison.OrdinalIgnoreCase));
            }

            if (false)
            {
                core.AddScript(driveConnection, "%userprofile%\\Documents\\AgenaTraderData", PeriodicitySettings.Empty);
                core.AddScript(dropboxConnection, "%userprofile%\\Documents\\AgenaTraderData", PeriodicitySettings.Empty);
            }

            foreach (var script in core.Scripts)
            {
                await script.PerformAsync();
            }

            if (false)
            {
                var backup = remoteBackupsState.Backups[0];

                await remoteBackupsState.Provider.DownloadAsync(backup,
                    System.IO.Path.Combine("%userprofile%\\Desktop\\Results", backup.BackupName));

                await remoteBackupsState.Provider.DeleteAsync(backup);

                core.RemoveScript(core.Scripts.First());

                var script = core.AddScript(core.Connections.First(), "%userprofile%\\Desktop\\Results",
                    PeriodicitySettings.Empty);

                await script.PerformAsync();
            }
        }
    }
}