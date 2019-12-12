using System;
using System.Threading.Tasks;
using NightKeeper.Helper.Backups;

namespace NightKeeper.Helper
{
    public interface IProvider
    {
        Guid Id { get; }

        string Name { get; }

        byte[] Logo { get; }

        object GetConnectionValues();

        Task<RemoteBackupsState> GetRemoteBackups();

        Task Upload(LocalBackup localBackup);

        Task DownloadAsync(RemoteBackupsState.RemoteBackup backup, string outputPath);

        Task DeleteAsync(RemoteBackupsState.RemoteBackup backup);
    }

    public class Phone
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"Phone: {this.Title}; price: {this.Price}";
        }
    }

}