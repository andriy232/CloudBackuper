using NightKeeper.Helper;
using NightKeeper.Helper.Core;
using System;
using System.Threading.Tasks;
using System.Windows;
using NightKeeper;

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
            _core = Core.Instance;
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

            foreach (var provider in _core.Providers)
                _core.Logger.Log($"Available provider: {provider}");

            foreach (var connection in _core.Connections)
                await AddConnection(connection);

            foreach (var script in _core.Scripts)
                AddScript(script);
        }

        private async Task AddConnection(IConnection connection)
        {
            var remoteBackupsState = await connection.GetRemoteBackups();
            _core.Logger.Log($"{connection}, {remoteBackupsState}");

            lvConnections.Items.Add(connection);
        }

        private void AddScript(Script script)
        {
            _core.Logger.Log(script.ToString());

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
                _core?.Logger.Log(ex);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var win2 = new CreateScript();
            win2.Show();
        }
    }
}