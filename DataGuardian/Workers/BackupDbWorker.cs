using System.Diagnostics;
using System.Linq;
using DataGuardian.DbLevel;
using DataGuardian.Impl;
using DataGuardian.Plugins;

namespace DataGuardian.Workers
{
    internal class BackupDbWorker
    {
        private readonly BackupsDbContext _dbContext;

        public BackupDbWorker(string dbConnection)
        {
            _dbContext = new BackupsDbContext(dbConnection);
        }

        public void Save(IBackupScript newScript)
        {
            _dbContext.BackupScripts.Add((BackupScript) newScript);
            _dbContext.SaveChanges();
        }

        public void Edit(IBackupScript newScript)
        {
            var entity = _dbContext.BackupScripts.FirstOrDefault(item => item.Id == newScript.Id);
            if (entity == null)
            {
                Debug.Assert(false);
                return;
            }

            _dbContext.Entry(entity).CurrentValues.SetValues(newScript);
            _dbContext.SaveChanges();
        }

        public void Delete(IBackupScript newScript)
        {
            _dbContext.BackupScripts.Remove((BackupScript) newScript);
            _dbContext.SaveChanges();
        }
    }
}