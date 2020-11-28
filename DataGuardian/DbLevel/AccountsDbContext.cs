using System.Data.Entity;
using DataGuardian.Plugins;

namespace DataGuardian.DbLevel
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext(string dbConnection) : base(dbConnection)
        {
        }

        public DbSet<CloudProviderAccount> Accounts { get; set; }
    }
}