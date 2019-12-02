using System;
using System.Linq;
using System.Threading.Tasks;
using Helper;
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
            foreach (var provider in Core.Providers)
            {
                Core.WriteLine($"Provider: {provider.Id} - {provider.Name}");
            }

            foreach (var provider in Core.Connections)
            {
                Core.WriteLine($"Connection: {provider}");
            }

            foreach (var script in Core.Scripts)
            {
                Core.WriteLine($"Script: {script}");
            }

            IConnection dropboxConnection;
            IConnection driveConnection;

            if (!Core.Connections.Any())
            {
                //          var drive = Core.Providers.First(x => x.Name.Contains("drive", StringComparison.OrdinalIgnoreCase));
                //          var driveValues = drive.GetValues();
                //
                //          driveConnection = Core.Database.AddConnection(
                //              "Google Drive",
                //              drive,
                //              driveValues);

                var dropbox = Core.Providers.First(x => x.Name.Contains("dropbox", StringComparison.OrdinalIgnoreCase));
                var dropboxValues = dropbox.GetValues();

                dropboxConnection = Core.Database.SaveConnection(
                    "Dropbox",
                    dropbox,
                    dropboxValues);
            }
            else
            {
                //           driveConnection =
                //              Core.Connections.First(x => x.Provider.Name.Contains("drive", StringComparison.OrdinalIgnoreCase));
                dropboxConnection =
                    Core.Connections.First(x =>
                        x.Provider.Name.Contains("dropbox", StringComparison.OrdinalIgnoreCase));
            }

            //    Core.AddScript(new Script(driveConnection, "%userprofile%\\Desktop\\temp.db", Periodicity.DateTime));
            Core.AddScript(dropboxConnection, "%userprofile%\\Desktop\\temp.db", PeriodicitySettings.Empty);

            foreach (var script in Core.Scripts)
            {
                await script.Perform();
            }
        }
    }
}