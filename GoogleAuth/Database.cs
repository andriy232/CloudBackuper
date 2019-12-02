using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Helper.Settings;
using Newtonsoft.Json;

namespace Helper
{
    public class Database
    {
        private const string DbFileName = "CloudBackuper.sqlite";
        private static string _databasePath;
        private static Database _instance;

        private Database()
        {
        }

        public static Database GetInstance()
        {
            return _instance ?? (_instance = new Database());
        }

        internal static void EnsureDatabase()
        {
            _databasePath = DbFileName;
            if (File.Exists(_databasePath))
                return;

            _databasePath = Path.Combine(Core._appFolder, DbFileName);
            if (File.Exists(_databasePath))
                return;

            var message = $"Config '{DbFileName}' is empty!";
            Core.WriteLine(message);
            InitDatabase();
        }

        private static SQLiteConnection CreateConnection()
        {
            SQLiteConnection connection;

            try
            {
                connection = new SQLiteConnection($"Data Source={_databasePath};Version=3;");
                connection.Open();
            }
            catch (Exception ex)
            {
                Core.WriteLine(ex);
                throw;
            }

            return connection;
        }

        public IEnumerable<IConnection> ReadConnections(IEnumerable<IProvider> providers)
        {
            var enumerable = providers.ToList();
            var list = new List<IConnection>();
            using (var connection = CreateConnection())
            {
                var command = connection.CreateCommand();
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

        public IConnection SaveConnection(string name, IProvider provider, object values)
        {
            int id;

            using (var connection = CreateConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    "insert into `connections` (`providerId`, `name`, `values`) values (@guid, @name, @values)";
                command.Parameters.AddWithValue("@guid", provider.Id.ToString());
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@values", values.ToString());
                command.ExecuteNonQuery();

                command.CommandText = "select last_insert_rowid()";
                var result = command.ExecuteScalar().ToString();
                int.TryParse(result, out id);
            }

            Debug.Assert(id != 0, "not inserted!");

            return new Connection(id, name, provider, values);
        }

        private static void InitDatabase()
        {
            if (File.Exists(_databasePath))
                return;

            SQLiteConnection.CreateFile(_databasePath);

            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    var sql = @"
CREATE TABLE IF NOT EXISTS `connections` ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `providerId` TEXT NOT NULL, `name` TEXT NOT NULL, `values` TEXT )";
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Script> ReadScripts(IEnumerable<IConnection> connections)
        {
            return Enumerable.Empty<Script>();
        }

        public T ReadSettings<T>(Guid id) where T : SettingsBase
        {
            using (var connection = CreateConnection())
            {
                var command = connection.CreateCommand();
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
            {
                var command = connection.CreateCommand();
                command.CommandText = "update `connections` set `value`=@value where guid like '@guid'";
                command.Parameters.AddWithValue("@value", settings.ToString());
                command.Parameters.AddWithValue("@guid", id.ToString());
                command.ExecuteNonQuery();
            }
        }

        public void SaveScript(Script script)
        {
        }
    }
}