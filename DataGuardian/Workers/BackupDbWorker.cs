using System.Collections.Generic;
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
            var backupScript = (BackupScript) newScript;
            backupScript.SetSerializedState();

            if (!Contains(backupScript))
            {
                _dbContext.BackupScripts.Add(backupScript);
                _dbContext.SaveChanges();
            }
        }

        private bool Contains(BackupScript backupScript)
        {
            return _dbContext.BackupScripts.ToList().Contains(backupScript);
        }

        public void Edit(IBackupScript newScript)
        {
            newScript.SetSerializedState();

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
            var backupScript = (BackupScript) newScript;
            if (Contains(backupScript))
            {
                _dbContext.BackupScripts.Remove(backupScript);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<IBackupScript> Read()
        {
            return _dbContext.BackupScripts.ToList();
        }
    }
}