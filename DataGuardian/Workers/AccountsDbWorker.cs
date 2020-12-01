using DataGuardian.DbLevel;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Plugins;
using System.Collections.Generic;
using System.Linq;

namespace DataGuardian.Workers
{
    public class AccountsDbWorker
    {
        private readonly AccountsDbContext _context;

        public AccountsDbWorker(string dbConnection)
        {
            _context = new AccountsDbContext(dbConnection);
        }

        public void SaveProvider(IAccount account)
        {
            if (!Contains(account))
            {
                _context.Accounts.Add((CloudProviderAccount) account);
                _context.SaveChanges();
            }
        }

        private bool Contains(IAccount cloudProviderAccount)
        {
            return _context.Accounts.ToList().Contains(cloudProviderAccount);
        }

        public void DeleteProvider(IAccount account)
        {
            if (Contains(account))
            {
                _context.Accounts.Remove((CloudProviderAccount) account);
                _context.SaveChanges();
            }
        }

        public IEnumerable<CloudProviderAccount> ReadAccounts()
        {
            return _context.Accounts.ToList();
        }
    }
}