using System;
using System.Collections.Generic;
using System.IO;
using DataGuardian.Server.Models;
using DataGuardian.Server.Plugins;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DataGuardian.Server.Impl
{
    public class AuthManager : IAuthManager
    {
        private readonly Settings _settings;
        private AuthData _authData = new AuthData();

        public AuthManager(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            ReadSettings();
        }

        private void ReadSettings()
        {
            if (File.Exists(_settings.UsersDataLocation))
            {
                var content = File.ReadAllText(_settings.UsersDataLocation);

                if (!string.IsNullOrWhiteSpace(content))
                    _authData = JsonConvert.DeserializeObject<AuthData>(content);
            }
        }

        public string GetUserDirectory(string uid)
        {
            return _authData.TryGetValue(uid, out var userData) ? userData.UserDirectory : string.Empty;
        }

        public string GetUserUid(string computerId)
        {
            if (string.IsNullOrWhiteSpace(computerId))
                throw new ArgumentException(nameof(computerId));

            var userUid = Guid.NewGuid().ToString();
            _authData[userUid] = new UserData
            {
                ComputerId = computerId,
                UserDirectory = Path.Combine(_settings.DataStorageLocation, Guid.NewGuid().ToString("N"))
            };
            SaveSettings();
            return userUid;
        }

        private void SaveSettings()
        {
            var content = JsonConvert.SerializeObject(_authData, Formatting.Indented);
            var directory = Path.GetDirectoryName(_settings.UsersDataLocation);

            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(_settings.UsersDataLocation, content);
        }

        public class AuthData : Dictionary<string, UserData>
        {
        }
    }
}