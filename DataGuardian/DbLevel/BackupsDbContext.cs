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
	""CreateDate""	TEXT NOT NULL,
	""Enabled""	INTEGER,
	""Name""	TEXT NOT NULL UNIQUE,
	""TargetPath""	TEXT NOT NULL,
    ""Steps"" TEXT,
	PRIMARY KEY(""Id"" AUTOINCREMENT)
);");
        }

        public DbSet<BackupScript> BackupScripts { get; set; }
    }
}