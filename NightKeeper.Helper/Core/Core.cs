using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using NightKeeper.Helper.Settings;
using Serilog;

namespace NightKeeper.Helper.Core
{
    public static class Core
    {
        internal static string _appFolder;

        public static IEnumerable<IProvider> Providers => providers;
        public static IEnumerable<IConnection> Connections => connections;
        public static IEnumerable<Script> Scripts => scripts;

        private static readonly List<IProvider> providers = new List<IProvider>();
        private static readonly List<IConnection> connections = new List<IConnection>();
        private static readonly List<Script> scripts = new List<Script>();

        public static readonly FileSystem FileSystem = FileSystem.GetInstance();
        public static readonly Database Database = Database.GetInstance();

        private static UnhandledExceptionHandler _handler;

        public static ILogger CurrentLog { get; private set; }

        public static string GetConfiguration()
        {
            return IsDebug() ? "Debug" : "Release";
        }

        private static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public static void Load()
        {
            _handler = new UnhandledExceptionHandler();

            InitAppFolder();
            InitLogs();

            Database.EnsureDatabase();

            EnsureProviders();
            connections.AddRange(Database.ReadConnections(Providers));
            scripts.AddRange(Database.ReadScripts(Connections));
        }

        private static void EnsureProviders()
        {
            var info = typeof(IProvider);

            var files = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.dll");

            foreach (var file in files)
            {
                try
                {
                    Assembly.LoadFile(file);
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly
                        .GetTypes()
                        .Where(x => x.IsPublic && !x.IsAbstract && x.GetInterfaces().Any(i => i == info));
                    foreach (var type in types)
                    {
                        if (Activator.CreateInstance(type) is IProvider instance)
                            providers.Add(instance);
                    }
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
            }

            if (providers.Count == 0)
                throw new ApplicationException("no providers");
        }

        private static void InitLogs()
        {
            var logsPath = Path.Combine(_appFolder, "Logs");

            var logFilePath = Path.Combine(logsPath, $"{GetExeName()}_{GetConfiguration()}_.log");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            CurrentLog = Log.Logger;
        }

        private static void InitAppFolder()
        {
            var path = GetAppDataPath();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            _appFolder = path;
        }

        public static string GetAppDataPath()
        {
            var exeName = GetExeName();
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                exeName);
            return path;
        }

        private static string GetExeName()
        {
            return Assembly.GetEntryAssembly()?.GetName().Name;
        }

        public static void WriteLine(string message)
        {
            Log.Information(message);
        }

        public static void WriteLine(Exception ex)
        {
            Log.Debug(ex, "Error");
        }

        public static void StartProcess(string uri)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Process.Start do not work in .Net Core in Windows
                var request = uri.Replace("&", "^&");
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = $"/c start {request}"
                };
                Process.Start(psi);
            }
            else
            {
                Process.Start(uri);
            }
        }

        public static Script AddScript(IConnection connection, string targetPath, PeriodSettings period)
        {
            var script = new Script(0, connection, targetPath, period);
            Database.SaveScript(script);
            scripts.Add(script);
            return script;
        }

        public static IConnection AddConnection(string name, IProvider provider, object connectionSettings)
        {
            var connection = new Connection(0, name, provider, connectionSettings);

            var exists = connections.FirstOrDefault(x => x.Equals(connection));
            if (exists != null)
                return exists;

            Database.SaveConnection(connection);
            connections.Add(connection);
            return connection;
        }

        public static void RemoveScript(Script script)
        {
            Database.RemoveScript(script);
            scripts.Remove(script);
        }

        public static void RemoveConnection(IConnection connection)
        {
            Database.RemoveConnection(connection);
            connections.Remove(connection);
        }

        public static string ReadLine(string message, Func<string, bool> validationFunc = null)
        {
            string input = null;

            while (string.IsNullOrWhiteSpace(input) || validationFunc != null && !validationFunc(input))
            {
                Console.WriteLine(message);
                input = Console.ReadLine();
            }

            return input;
        }
    }
}