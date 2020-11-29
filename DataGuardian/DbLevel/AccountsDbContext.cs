using DataGuardian.Plugins;
using System.Data.Entity;
using System.Data.SQLite;

namespace DataGuardian.DbLevel
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext(string dbConnection) :
            base(new SQLiteConnection(dbConnection), true)
        {
            Database.ExecuteSqlCommand(
                @"CREATE TABLE IF NOT EXISTS ""Accounts"" (
	""Id""	INTEGER NOT NULL UNIQUE,
	""Name""	TEXT NOT NULL UNIQUE,
	""ConnectionSettings""	TEXT NOT NULL,
	""CloudId""	TEXT NOT NULL,
	PRIMARY KEY(""Id"" AUTOINCREMENT)
);");
        }

        public DbSet<CloudProviderAccount> Accounts { get; set; }
    }
}