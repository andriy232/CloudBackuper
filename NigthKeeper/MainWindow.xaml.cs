using NightKeeper.Helper;
using NightKeeper.Helper.Core;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace NigthKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Core _core;

        public MainWindow()
        {
            _core = Core.GetInstance();
            InitializeComponent();

            Title = _core.GetTitle();

            _core.RegisterOutput(LogOutput, RequestStringInput);
        }

        private string RequestStringInput(string message, Func<string, bool> validationfunc)
        {
            return new InputBox(message, Title, validationfunc).ShowDialog();
        }

        private async Task LoadData()
        {
            lvLogOutput.ItemsSource = null;
            lvConnections.ItemsSource = null;
            lvScripts.ItemsSource = null;

            _core.Load();

            foreach (var provider in _core.Providers)
                _core.Log($"Available provider: {provider}");

            foreach (var connection in _core.Connections)
                await AddConnection(connection);

            foreach (var script in _core.Scripts)
                AddScript(script);
        }

        private async Task AddConnection(IConnection connection)
        {
            var remoteBackupsState = await connection.GetRemoteBackups();
            _core.Log($"{connection}, {remoteBackupsState}");

            lvConnections.Items.Add(connection);
        }

        private void AddScript(Script script)
        {
            _core.Log(script.ToString());

            lvScripts.Items.Add(script);
        }

        private void LogOutput(object sender, LogEntry logEntry)
        {
            if (logEntry == null)
                return;

            lvLogOutput.Items.Insert(0, logEntry);
        }

        private async void OnRootGridLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await LoadData();
            }
            catch (Exception ex)
            {
                _core?.Log(ex);
            }
        }
    }
}