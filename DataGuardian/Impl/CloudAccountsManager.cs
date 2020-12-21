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
        private readonly List<IAccount> _accounts = new List<IAccount>();
        public event EventHandler<AccountsChangedEventArgs> AccountsChanged;
        private AccountsDbWorker _dbWorker;
        public IEnumerable<IAccount> Accounts => _accounts;

        public override string Name => "Cloud Accounts";

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
                       
                        if(Core.CloudAccountsManager.Accounts.Any(x=>x.Name.Trim().Equals(name, StringComparison.Ordinal)))
                           throw new ApplicationException("Such account exists");

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
                GuiHelper.ShowMessage($"Cannot add account, error: {ex.Message}");
                Core?.Logger?.Log("AddAccount", ex);
            }
        }

        public void RemoveAccount(IAccount selectedCloudProvider)
        {
            try
            {
                if (selectedCloudProvider == null)
                    return;

                _dbWorker.DeleteProvider(selectedCloudProvider);
                _accounts.Remove(selectedCloudProvider);
                FireAccountsChanged();
            }
            catch (Exception ex)
            {
                Core?.Logger?.Log("RemoveAccount", ex);
            }
        }

        private void FireAccountsChanged()
        {
            AccountsChanged?.Invoke(this, new AccountsChangedEventArgs(_accounts));
        }

        public T GetSettings<T>(IAccount account) where T : SettingsBase
        {
            var settingsState = account.ConnectionSettings.ToString();
            return JsonConvert.DeserializeObject<T>(settingsState);
        }
    }
}