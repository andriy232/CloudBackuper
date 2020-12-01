using DataGuardian.Impl;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using DataGuardian.Plugins.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DataGuardian
{
    public class Core : ICore
    {
        private readonly List<ICloudStorageProvider> _cloudProviders = new List<ICloudStorageProvider>(2);
        private readonly UnhandledExceptionHandler _handler;

        public IBackupManager BackupManager { get; }

        public ICloudAccountsManager CloudAccountsManager { get; }

        public IEnumerable<ICloudStorageProvider> CloudStorageProviders => _cloudProviders;
        
        public IGuiManager GuiManager { get; }

        public ISettings Settings { get; }

        public ISingleLogger Logger { get; }

        public Core()
        {
            _handler = new UnhandledExceptionHandler();
            Settings = new UserSettings();
            Logger = new SingleLogger();
            BackupManager = new BackupManager();
            CloudAccountsManager = new CloudAccountsManager();
            GuiManager = new GuiManager();

            CoreStatic.SetCore(this);

            Start();
        }

        private void Start()
        {
            _cloudProviders.Clear();

            var files = GetAssembliesToLoadPlugins();
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                _cloudProviders.AddRange(GetObjectsFromAssembly<ICloudStorageProvider>(assembly));
            }

            var pluginsToInit = GetPluginsToInit();
            foreach (var plugin in pluginsToInit)
            {
                plugin.Init(this);
            }
        }

        private static IEnumerable<string> GetAssembliesToLoadPlugins()
        {
            return FileSystem.GetAllFiles(Directory.GetCurrentDirectory())
                .Where(x =>
                {
                    var fileName = Path.GetFileNameWithoutExtension(x);
                    var extension = Path.GetExtension(x);
                    return !x.Contains("netcoreapp") && fileName.Contains("DataGuardian") &&
                           new[] {".dll", ".exe"}.Contains(extension);
                });
        }

        private IEnumerable<IPlugin> GetPluginsToInit()
        {
            return new IPlugin[] {_handler, Settings, Logger, BackupManager, CloudAccountsManager}
                .Union(_cloudProviders);
        }

        private IEnumerable<T> GetObjectsFromAssembly<T>(Assembly assembly) where T : class
        {
            var findType = typeof(T);

            var types = assembly.GetTypes();

            var list = new List<T>();
            foreach (var type in types)
            {
                if (!type.IsPublic || (type.Attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract)
                    continue;

                foreach (var interfaceType in type.GetInterfaces())
                {
                    if (interfaceType == findType)
                        list.Add(Activator.CreateInstance(type) as T);
                }
            }

            return list;
        }
    }
}