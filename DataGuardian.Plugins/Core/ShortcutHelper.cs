using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace DataGuardian.Plugins.Core
{
    public static class ShortcutHelper
    {
        public class CreateShortcutParams
        {
            public string Name { get; set; }
            public string DestinationDirectory { get; set; }
            public string WorkingDirectory { get; set; }
            public string ExeFile { get; set; }
            public Icon Icon { get; set; }
            public string Description { get; set; }
            public string[] CommandLineArgs { get; set; }
        }

        public static void ToggleAutoStartShortcut(Icon applicationIcon)
        {
            try
            {
                const Environment.SpecialFolder startupFolder = Environment.SpecialFolder.Startup;
                var name = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly()?.Location);

                var dir = Environment.GetFolderPath(startupFolder);

                if (CheckIfShortcutExist(dir, name))
                    FileSystem.Delete(BuildShortcutPath(dir, name));
                else
                    PutShortcut(startupFolder, applicationIcon);
            }
            catch (Exception ex)
            {
                CoreStatic.Instance?.Logger?.Log($"Error during shortcut creation: {ex}");
            }
        }

        public static bool CheckIfShortcutExist()
        {
            try
            {
                const Environment.SpecialFolder startupFolder = Environment.SpecialFolder.Startup;
                var name = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly()?.Location);

                var dir = Environment.GetFolderPath(startupFolder);

                return CheckIfShortcutExist(dir, name);
            }
            catch (Exception ex)
            {
                CoreStatic.Instance?.Logger?.Log($"Error during shortcut check: {ex}");
                return false;
            }
        }

        private static void PutShortcut(Environment.SpecialFolder specialFolder, Icon applicationIcon)
        {
            var specialFolderPath = Environment.GetFolderPath(specialFolder);
            var exeFile = Assembly.GetEntryAssembly()?.Location;

            var createShortcutParams = new CreateShortcutParams
            {
                Name = CoreStatic.Instance.GuiManager.MainWindow.Text,
                ExeFile = exeFile,
                Description = CoreStatic.Instance.GuiManager.MainWindow.Text,
                DestinationDirectory = specialFolderPath,
                Icon = applicationIcon
            };
            PutShortcutIfNotExist(createShortcutParams);
        }

        public static void PutShortcutIfNotExist(CreateShortcutParams createShortcutParams)
        {
            if (CheckIfShortcutExist(createShortcutParams.DestinationDirectory, createShortcutParams.Name))
                return;

            if (string.IsNullOrWhiteSpace(createShortcutParams.ExeFile))
                return;

            var workingDir = createShortcutParams.WorkingDirectory;
            if (string.IsNullOrWhiteSpace(workingDir))
                workingDir = Path.GetDirectoryName(createShortcutParams.ExeFile);

            if (string.IsNullOrWhiteSpace(workingDir))
            {
                Debug.Assert(!string.IsNullOrWhiteSpace(workingDir), "working dir is null");
                return;
            }

            var args = string.Empty;
            if (createShortcutParams.CommandLineArgs != null && createShortcutParams.CommandLineArgs.Length > 0)
                args = string.Join(" ", createShortcutParams.CommandLineArgs);

            var iconLocation = Path.Combine(Path.GetTempPath(), "icon.ico");
            using (var icoStream = File.OpenWrite(iconLocation))
                createShortcutParams.Icon.Save(icoStream);

            var path = BuildShortcutPath(createShortcutParams.DestinationDirectory, createShortcutParams.Name);

            var wsh = new WshShell();
            var shortcut = wsh.CreateShortcut(path) as IWshShortcut;
            if (shortcut == null)
                return;

            shortcut.Arguments = args;
            shortcut.TargetPath = createShortcutParams.ExeFile;
            shortcut.WindowStyle = 1;
            shortcut.Description = createShortcutParams.Description ?? createShortcutParams.Name;
            shortcut.WorkingDirectory = workingDir;
            shortcut.IconLocation = iconLocation;
            shortcut.Save();
        }

        private static string BuildShortcutPath(string directory, string name)
        {
            return Path.Combine(directory, name + ".lnk");
        }

        private static bool CheckIfShortcutExist(string dir, string name)
        {
            var path = BuildShortcutPath(dir, name);
            return File.Exists(path);
        }
    }
}