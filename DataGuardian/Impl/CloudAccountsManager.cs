using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DataGuardian.GUI;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Plugins;
using DataGuardian.Windows;
using DataGuardian.Workers;
using Newtonsoft.Json;

namespace DataGuardian.Impl
{
    public class CloudAccountsManager : PluginBase, ICloudAccountsManager
    {
        private readonly List<ICloudProviderAccount> _accounts = new List<ICloudProviderAccount>();
        public event EventHandler<AccountsChangedEventArgs> AccountsChanged;
        private AccountsDbWorker _dbWorker;
        public IEnumerable<ICloudProviderAccount> Accounts => _accounts;

        public override void Init(ICore core)
        {
            base.Init(core);
            _dbWorker = new AccountsDbWorker(Core.Settings.ConnectionString);
            
            _accounts.AddRange(_dbWorker.ReadAccounts());
        }

        public void AddAccount(object form = null)
        {
            try
            {
                using (var dlg = new WndSelectCloudProvider())
                {
                    DialogResult dialogResult;
                    if (form is Control frm)
                        dialogResult = dlg.ShowDialog(frm);
                    else
                        dialogResult = dlg.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        var selectedProvider = dlg.ActiveCloudProvider;
                        var name = GuiHelper.ReadLine("Please enter name for new cloud account");
                        var connectionInfo = selectedProvider.TryAuth();
                        var newAccount = new CloudProviderAccount(name, selectedProvider, connectionInfo.ToString());

                        _dbWorker.SaveProvider(newAccount);
                        _accounts.Add(newAccount);
                        FireAccountsChanged();
                    }
                }
            }
            catch (Exception ex)
            {
                Core?.Logger?.Log(ex);
            }
        }

        public void RemoveAccount(ICloudProviderAccount selectedCloudProvider)
        {
            _dbWorker.DeleteProvider(selectedCloudProvider);
            _accounts.Remove(selectedCloudProvider);
            FireAccountsChanged();
        }

        private void FireAccountsChanged()
        {
            AccountsChanged?.Invoke(this, new AccountsChangedEventArgs(_accounts));
        }

        public T GetSettings<T>(ICloudProviderAccount account) where T : SettingsBase
        {
            var settingsState = account.ConnectionSettings.ToString();
            return JsonConvert.DeserializeObject<T>(settingsState);
        }
    }
}