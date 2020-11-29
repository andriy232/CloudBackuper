using System.Data.Entity;
using System.Data.SQLite;
using DataGuardian.Impl;

namespace DataGuardian.DbLevel
{
    internal class BackupsDbContext : DbContext
    {
        public BackupsDbContext(string dbConnection) :
            base(new SQLiteConnection(dbConnection), true)
        {
            Database.ExecuteSqlCommand(@"CREATE TABLE IF NOT EXISTS ""BackupScripts"" (
	""Id""	INTEGER NOT NULL UNIQUE,
	""CreateTime""	TEXT NOT NULL,
	""Enabled""	INTEGER,
	""Name""	TEXT NOT NULL UNIQUE,
	""BackupFileName""	TEXT NOT NULL UNIQUE,
	""TargetPath""	TEXT NOT NULL,
	PRIMARY KEY(""Id"" AUTOINCREMENT)
);");
            Database.ExecuteSqlCommand(@"CREATE TABLE IF NOT EXISTS ""BackupSteps"" (
	""Id""	INTEGER NOT NULL UNIQUE,
	""AccountId""	INTEGER,
	""TargetPath""	TEXT,
	""Action""	INTEGER NOT NULL,
	""ActionParameter""	TEXT,
	""Period""	INTEGER NOT NULL,
	""RecurEvery""	INTEGER,
	""StartDate""	TEXT NOT NULL,
	""PerformDate""	TEXT NOT NULL,
	""PeriodParameters""	TEXT,
	""LastState""	TEXT NOT NULL,
	FOREIGN KEY(""AccountId"") REFERENCES ""Accounts""(""Id""),
	PRIMARY KEY(""Id"" AUTOINCREMENT)
);");
        }

        public DbSet<BackupScript> BackupScripts { get; set; }
    }
}