using System.Data.Entity;
using DataGuardian.Impl;

namespace DataGuardian.DbLevel
{
    internal class BackupsDbContext : DbContext
    {
        public BackupsDbContext(string dbConnection) : base(dbConnection)
        {
        }

        public DbSet<BackupScript> BackupScripts { get; set; }
    }
}