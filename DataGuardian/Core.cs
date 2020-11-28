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
        public IBackupManager BackupManager { get; }
        public ICloudAccountsManager CloudAccountsManager { get; }
        public IEnumerable<ICloudStorageProvider> CloudStorageProviders => _cloudProviders;
        public ISettings Settings { get; }
        public ISingleLogger Logger { get; }

        public Core()
        {
            Settings = new UserSettings();
            Logger = new SingleLogger();
            BackupManager = new BackupManager();
            CloudAccountsManager = new CloudAccountsManager();
            
            Start();
        }

        public void Start()
        {
            _cloudProviders.Clear();

            var files = FileSystem.GetAllFiles(Directory.GetCurrentDirectory())
                .Where(x =>
                {
                    var fileName = Path.GetFileNameWithoutExtension(x);
                    var extension = Path.GetExtension(x);
                    return !x.Contains("netcoreapp") && fileName.Contains("DataGuardian") &&
                           new[] {".dll", ".exe"}.Contains(extension);
                });
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                _cloudProviders.AddRange(GetObjectsFromAssembly<ICloudStorageProvider>(assembly));
            }

            foreach (var plugin in new IPlugin[] {Settings, Logger, BackupManager, CloudAccountsManager}.Union(
                _cloudProviders))
            {
                plugin.Init(this);
            }
        }

        private IEnumerable<T> GetObjectsFromAssembly<T>(Assembly assembly) where T : class
        {
            var findType = typeof(T);

            var types = assembly.GetTypes();

            var list = new List<T>();
            foreach (Type type in types)
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