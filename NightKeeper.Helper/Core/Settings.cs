using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NightKeeper.Helper.Settings;

namespace NightKeeper.Helper.Core
{
    public class Settings
    {
        private const string DbFileName = "Store.sqlite";
        private readonly Core _core;
        private static string _databasePath;
        private static Settings _instance;

        private Settings(Core core)
        {
            _core = core;
        }

        public static Settings GetInstance(Core core)
        {
            return _instance ?? (_instance = new Settings(core));
        }

        internal void EnsureDatabase()
        {
            _databasePath = DbFileName;
            if (File.Exists(_databasePath))
                return;

            _databasePath = Path.Combine(_core.AppFolder, DbFileName);

            if (!File.Exists(_databasePath))
                _core.Log($"Database '{DbFileName}' not exist!");

            InitDatabaseIfNeed();
        }

        private void InitDatabaseIfNeed()
        {
            if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);
            }

            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
CREATE TABLE IF NOT EXISTS `connections` ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `providerId` TEXT NOT NULL, `name` TEXT NOT NULL, `values` TEXT )";
                command.ExecuteNonQuery();

                command.CommandText = @"
CREATE TABLE IF NOT EXISTS `scripts` ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `connectionId` INTEGER NOT NULL, `name` TEXT, `backupName` TEXT, `targetPath` TEXT NOT NULL, `period` TEXT,
FOREIGN KEY(`connectionId`) REFERENCES `connnections`(`id`))";
                command.ExecuteNonQuery();
            }
        }

        private SQLiteConnection CreateConnection()
        {
            SQLiteConnection connection;

            try
            {
                connection = new SQLiteConnection($"Data Source={_databasePath};Version=3;");
                connection.Open();
            }
            catch (Exception ex)
            {
                _core.Log(ex);
                throw;
            }

            return connection;
        }

        #region Connections

        public IEnumerable<IConnection> ReadConnections(IEnumerable<IStorageProvider> providers)
        {
            var enumerable = providers.ToList();
            var list = new List<IConnection>();

            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "select c.`id`, c.`name`, c.`providerId`, c.`values` from `connections` as c";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var name = reader.GetString(1);
                        var guid = reader.GetString(2);
                        var values = reader.GetString(3);

                        var conn = new Connection(id,
                            name,
                            enumerable.First(x => x.Id.ToString().Equals(guid, StringComparison.OrdinalIgnoreCase)),
                            values);

                        list.Add(conn);
                    }
                }
            }

            return list;
        }

        public IConnection SaveConnection(Connection conn)
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                if (conn.Id == 0)
                {
                    command.CommandText =
                        "insert into `connections` (`providerId`, `name`, `values`) values (@guid, @name, @values)";
                    command.Parameters.AddWithValue("@guid", conn.StorageProvider.Id.ToString());
                    command.Parameters.AddWithValue("@name", conn.Name);
                    command.Parameters.AddWithValue("@values", conn.ConnectionSettings.ToString());
                    command.ExecuteNonQuery();
                    conn.SetId((int) connection.LastInsertRowId);
                }
                else
                {
                    command.CommandText =
                        "update `connections` set `providerId`=@guid, `name`=@name, `values`=@values where `id`=@id";
                    command.Parameters.AddWithValue("@id", conn.Id);
                    command.Parameters.AddWithValue("@guid", conn.StorageProvider.Id.ToString());
                    command.Parameters.AddWithValue("@name", conn.Name);
                    command.Parameters.AddWithValue("@values", conn.ConnectionSettings.ToString());
                    command.ExecuteNonQuery();
                }
            }

            return conn;
        }

        public void RemoveConnection(IConnection conn)
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "delete from `connections` where `id`=@id";
                command.Parameters.AddWithValue("@id", conn.Id);
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Scripts

        public IEnumerable<Script> ReadScripts(IEnumerable<IConnection> connections)
        {
            var enumerable = connections.ToList();
            var list = new List<Script>();

            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "select s.`id`, s.`connectionId`, s.`name`, s.`backupName`, s.`targetPath`, s.`period` from `scripts` as s";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var connectionId = reader.GetInt32(1);
                        var name = reader.GetString(2);
                        var backupName = reader.GetString(3);
                        var targetPath = reader.GetString(4);
                        var period = reader.GetString(5);

                        var script = new Script(id,
                            enumerable.First(x => x.Id == connectionId),
                            targetPath,
                            PeriodicitySettings.Parse(period),
                            backupName,
                            name);

                        list.Add(script);
                    }
                }
            }

            return list;
        }

        public void SaveScript(Script script)
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                if (script.Id != 0)
                {
                    command.CommandText = @"update `scripts` set 
`name`=@name, 
`connectionId`=@connectionId,
`backupName`=@backupName, 
`targetPath`=@targetPath,
`period`=@period 
where `id`=@id";
                    command.Parameters.AddWithValue("@id", script.Id);
                    command.Parameters.AddWithValue("@connectionId", script.Connection.Id);
                    command.Parameters.AddWithValue("@backupName", script.BackupFileName);
                    command.Parameters.AddWithValue("@targetPath", script.TargetPath);
                    command.Parameters.AddWithValue("@period", script.Period);
                    command.Parameters.AddWithValue("@name", script.Name);
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText =
                        @"insert into `scripts` (`name`, `connectionId`, `backupName`, `targetPath`, `period`)
values (@name, @connectionId, @backupName, @targetPath, @period)";
                    command.Parameters.AddWithValue("@connectionId", script.Connection.Id);
                    command.Parameters.AddWithValue("@backupName", script.BackupFileName);
                    command.Parameters.AddWithValue("@targetPath", script.TargetPath);
                    command.Parameters.AddWithValue("@period", script.Period);
                    command.Parameters.AddWithValue("@name", script.Name);
                    command.ExecuteNonQuery();

                    script.SetId((int) connection.LastInsertRowId);
                }
            }
        }

        public void RemoveScript(Script script)
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "delete from `scripts` where `id`=@id";
                command.Parameters.AddWithValue("@id", script.Id);
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Settings

        #endregion

        public T ReadSettings<T>(Guid id) where T : SettingsBase
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select `values` from `connections` where `providerId` like @guid";
                command.Parameters.AddWithValue("@guid", id.ToString());

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var value = reader.GetString(0);
                        Debug.Assert(!string.IsNullOrWhiteSpace(value), "value is null");

                        return JsonConvert.DeserializeObject<T>(value);
                    }
                }
            }

            return null;
        }

        public void SaveSettings<T>(Guid id, T settings) where T : SettingsBase
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "update `connections` set `values`=@value where providerId like @guid";
                command.Parameters.AddWithValue("@value", settings.ToString());
                command.Parameters.AddWithValue("@guid", id.ToString());
                command.ExecuteNonQuery();
            }
        }
    }
}