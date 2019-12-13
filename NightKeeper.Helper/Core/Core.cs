using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using Dropbox.Api.TeamLog;
using NightKeeper.Helper.Settings;
using Serilog;

namespace NightKeeper.Helper.Core
{
    public class Core
    {
        internal string AppFolder;

        public IEnumerable<IProvider> Providers => _providers;
        public IEnumerable<IConnection> Connections => _connections;
        public IEnumerable<Script> Scripts => scripts;

        private readonly List<IProvider> _providers = new List<IProvider>();
        private readonly List<IConnection> _connections = new List<IConnection>();
        private readonly List<Script> scripts = new List<Script>();

        public readonly FileSystem FileSystem = FileSystem.GetInstance();
        public readonly Settings Settings;

        private UnhandledExceptionHandler _handler;

        private static readonly object Locker = new object();
        private static Core _instance;
        private InputRequestDelegate _inputRequestor;

        public delegate string InputRequestDelegate(string message, Func<string,bool> validationFunc);

        public static Core GetInstance()
        {
            lock (Locker)
            {
                return _instance ?? (_instance = new Core());
            }
        }

        private Core()
        {
            Settings = Settings.GetInstance(this);
        }

        public ILogger CurrentLog { get; private set; }

        public string GetConfiguration()
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

        public void Load()
        {
            _handler = new UnhandledExceptionHandler();

            InitAppFolder();
            InitLogs();

            Settings.EnsureDatabase();

            EnsureProviders();
            _connections.AddRange(Settings.ReadConnections(Providers));
            scripts.AddRange(Settings.ReadScripts(Connections));
        }

        private void EnsureProviders()
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
                    Log(ex);
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
                        {
                            instance.Init(this);
                            _providers.Add(instance);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            }

            if (_providers.Count == 0)
                throw new ApplicationException("no providers");
        }

        private void InitLogs()
        {
            var logsPath = Path.Combine(AppFolder, "Logs");

            var logFilePath = Path.Combine(logsPath, $"{GetExeName()}_{GetConfiguration()}_.log");
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            CurrentLog = Serilog.Log.Logger;
        }

        private void InitAppFolder()
        {
            var path = GetAppDataPath();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            AppFolder = path;
        }

        public static string GetAppDataPath()
        {
            var exeName = GetExeName();
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                exeName);
            return path;
        }

        public string GetTitle()
        {
            return $"{GetExeName()} - {GetConfiguration()}";
        }

        private static string GetExeName()
        {
            return Assembly.GetEntryAssembly()?.GetName().Name;
        }

        public event EventHandler<LogEntry> NewLog;
        private const string Default = "Default";

        public void Log(string message)
        {
            NewLog?.Invoke(this, new LogEntry(InfoLogType.Info, Default, message));
            Serilog.Log.Information(message);
        }

        public void Log(Exception ex)
        {
            NewLog?.Invoke(this, new LogEntry(InfoLogType.Error, Default, ex.ToString()));
            Serilog.Log.Debug(ex, "Error");
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

        public Script AddScript(IConnection connection, string targetPath, PeriodicitySettings period)
        {
            var script = new Script(0, connection, targetPath, period);
            Settings.SaveScript(script);
            scripts.Add(script);
            return script;
        }

        public IConnection AddConnection(string name, IProvider provider, object connectionSettings)
        {
            var connection = new Connection(0, name, provider, connectionSettings);

            var exists = _connections.FirstOrDefault(x => x.Equals(connection));
            if (exists != null)
                return exists;

            Settings.SaveConnection(connection);
            _connections.Add(connection);
            return connection;
        }

        public void RemoveScript(Script script)
        {
            Settings.RemoveScript(script);
            scripts.Remove(script);
        }

        public void RemoveConnection(IConnection connection)
        {
            Settings.RemoveConnection(connection);
            _connections.Remove(connection);
        }

        public string ReadLine(string message, Func<string, bool> validationFunc = null)
        {
            if (_inputRequestor != null)
                return _inputRequestor(message, validationFunc);

            string input = null;

            while (string.IsNullOrWhiteSpace(input) || validationFunc != null && !validationFunc(input))
            {
                Console.WriteLine(message);
                input = Console.ReadLine();
            }

            return input;
        }
        
        public void RegisterOutput(EventHandler<LogEntry> action, InputRequestDelegate inputRequestDelegate)
        {
            _inputRequestor = inputRequestDelegate;
            NewLog += action;
        }
    }
}