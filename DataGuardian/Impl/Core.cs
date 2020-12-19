using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Core;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Impl
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
            PerformSystemHacks();

            Start();
        }

        private static void PerformSystemHacks()
        {
            try
            {
                ServicePointManager.Expect100Continue = true;

                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                ServicePointManager.ServerCertificateValidationCallback = ServerCertificateValidationCallback;
            }
            catch
            {
                // ignored
            }
        }

        private static bool ServerCertificateValidationCallback(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors errors)
        {
            return true;
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
            var hashSet = new HashSet<string>();
            foreach (var plugin in pluginsToInit)
            {
                if (string.IsNullOrWhiteSpace(plugin.Name))
                    throw new ApplicationException("Name cannot be null");
                if (!hashSet.Add(plugin.Name))
                    throw new ApplicationException($"duplicated plugin: {plugin.Name}");

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

        private static IEnumerable<T> GetObjectsFromAssembly<T>(Assembly assembly) where T : class
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