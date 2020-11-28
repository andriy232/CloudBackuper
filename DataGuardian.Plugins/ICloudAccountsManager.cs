using System;
using System.Collections.Generic;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Plugins
{
    public interface ICloudAccountsManager : IPlugin
    {
        void AddAccount(object parentForm = null);

        void RemoveAccount(ICloudProviderAccount selectedCloudProvider);

        T GetSettings<T>(ICloudProviderAccount id) where T : SettingsBase;

        IEnumerable<ICloudProviderAccount> Accounts { get; }

        event EventHandler<AccountsChangedEventArgs> AccountsChanged;
    }

    public class AccountsChangedEventArgs : EventArgs
    {
        public IEnumerable<ICloudProviderAccount> Accounts { get; }

        public AccountsChangedEventArgs(IEnumerable<ICloudProviderAccount> accounts)
        {
            Accounts = accounts ?? throw new ArgumentException(nameof(accounts));
        }
    }
}