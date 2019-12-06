using System;
using System.Linq;
using System.Threading.Tasks;
using Helper;
using Helper.Backups;
using Helper.Core;
using Helper.Settings;

namespace CloudBackuper
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                Core.Load();

                new Program().Go().GetAwaiter().GetResult();

                Core.WriteLine("Program returned 0");
            }
            catch (Exception ex)
            {
                Core.WriteLine(ex);
                Core.WriteLine("Program returned -1");
                Console.ReadLine();
            }
        }

        private async Task Go()
        {
            RemoteBackupsState remoteBackupsState = null;

            foreach (var provider in Core.Providers)
            {
                Core.WriteLine($"Available provider: {provider}");
            }

            foreach (var connection in Core.Connections)
            {
                remoteBackupsState = await connection.GetRemoteBackups();
                Core.WriteLine($"{connection}, {remoteBackupsState}");
            }

            foreach (var script in Core.Scripts)
            {
                Core.WriteLine(script.ToString());
            }

            IConnection dropboxConnection;
            IConnection driveConnection;

            if (true)
            {
                var drive = Core.Providers.First(x => x.Name.Contains("drive", StringComparison.OrdinalIgnoreCase));
                var driveValues = drive.GetConnectionValues();

                driveConnection = Core.AddConnection(
                    "Google Drive",
                    drive,
                    driveValues);

                var dropbox = Core.Providers.First(x => x.Name.Contains("dropbox", StringComparison.OrdinalIgnoreCase));
                var dropboxValues = dropbox.GetConnectionValues();

                dropboxConnection = Core.AddConnection(
                    "Dropbox",
                    dropbox,
                    dropboxValues);
            }
            else
            {
                driveConnection =
                    Core.Connections.First(x => x.Provider.Name.Contains("drive", StringComparison.OrdinalIgnoreCase));
                dropboxConnection =
                    Core.Connections.First(x =>
                        x.Provider.Name.Contains("dropbox", StringComparison.OrdinalIgnoreCase));
            }

            if (false)
            {
                Core.AddScript(driveConnection, "%userprofile%\\Documents\\AgenaTraderData", PeriodSettings.Empty);
                Core.AddScript(dropboxConnection, "%userprofile%\\Documents\\AgenaTraderData", PeriodSettings.Empty);
            }

            foreach (var script in Core.Scripts)
            {
                await script.PerformAsync();
            }

            if (false)
            {
                var backup = remoteBackupsState.Backups[0];

                await remoteBackupsState.Provider.DownloadAsync(backup,
                    System.IO.Path.Combine("%userprofile%\\Desktop\\Results", backup.BackupName));

                await remoteBackupsState.Provider.DeleteAsync(backup);

                Core.RemoveScript(Core.Scripts.First());

                var script = Core.AddScript(Core.Connections.First(), "%userprofile%\\Desktop\\Results",
                    PeriodSettings.Empty);

                await script.PerformAsync();
            }
        }
    }
}