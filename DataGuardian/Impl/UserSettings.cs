using System;
using System.IO;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;

namespace DataGuardian.Impl
{
    public class UserSettings : PluginBase, ISettings
    {
        public override string Name => "Settings";

        public string ConnectionString { get; }

        public string DataDirectory { get; }

        public UserSettings()
        {
            DataDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "DataGuardian");

            FileSystem.CreateDirectoryIfNotExists(DataDirectory);

            ConnectionString = $"Data Source={Path.Combine(DataDirectory, "DataGuardian.sqlite")};";
        }
    }
}