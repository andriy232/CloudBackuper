using System;
using System.Threading.Tasks;

namespace Helper
{
    public interface IProvider
    {
        Guid Id { get; }
        string Name { get; }

        Task<BackupState> GetExistingBackups();
        Task Upload(Backup backup);

        object GetValues();
    }
}