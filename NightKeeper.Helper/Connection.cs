using System.Threading.Tasks;
using NightKeeper.Helper.Backups;

namespace NightKeeper.Helper
{
    public class Connection : IConnection
    {
        public IProvider Provider { get; }
        public object ConnectionSettings { get; }
        public string Name { get; }
        public int Id { get; private set; }

        public Task<RemoteBackupsState> GetRemoteBackups()
        {
            return Provider.GetRemoteBackups();
        }

        public Task Upload(LocalBackup localBackup)
        {
            return Provider.Upload(localBackup);
        }

        public Connection(int id, string name, IProvider provider, object connectionSettings)
        {
            Id = id;
            Provider = provider;
            ConnectionSettings = connectionSettings;
            Name = name;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"Connection: {Name}, {Provider.Name}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Connection conn)
                return IsSame(this, conn);
            return false;
        }

        private bool IsSame(Connection conn1, Connection conn2)
        {
            return conn1.Provider.Equals(conn2.Provider) &&
                   conn1.Name.Equals(conn2.Name);
        }
    }
}