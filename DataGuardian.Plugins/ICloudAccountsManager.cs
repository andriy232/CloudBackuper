using System;
using System.Collections.Generic;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.Plugins
{
    public interface ICloudAccountsManager : IPlugin
    {
        void AddAccount(object parentForm = null);

        void RemoveAccount(IAccount selectedCloudProvider);

        T GetSettings<T>(IAccount id) where T : SettingsBase;

        IEnumerable<IAccount> Accounts { get; }

        event EventHandler<AccountsChangedEventArgs> AccountsChanged;
    }

    public class AccountsChangedEventArgs : EventArgs
    {
        public IEnumerable<IAccount> Accounts { get; }

        public AccountsChangedEventArgs(IEnumerable<IAccount> accounts)
        {
            Accounts = accounts ?? throw new ArgumentException(nameof(accounts));
        }
    }
}