using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGuardian.Plugins.Backups;
using DataGuardian.Server.Models;
using DataGuardian.Server.Plugins;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace DataGuardian.Server.Impl
{
    public class StorageManager : IStorageManager
    {
        private readonly Settings _settings;

        public StorageManager(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SaveFileToFileSystem(string userDirectory, string fileName, Stream stream)
        {
            try
            {
                var directory = userDirectory;
                if (string.IsNullOrWhiteSpace(directory))
                    directory = Path.GetTempPath();

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var filePath = Path.Combine(directory, fileName);
                await using var fileStream = File.OpenWrite(filePath);

                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(fileStream);

                stream.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during saving file to storage");
            }

            //  using var request = new HttpRequestMessage()
            //  {
            //      Method = HttpMethod.Post,
            //      RequestUri = new Uri("YOUR_DESTINATION_URI"),
            //      Content = new StreamContent(stream),
            //  };
            //  using var response = await new HttpClient().SendAsync(request);
        }

        public async Task<MemoryStream> ReadFile(string userDirectory, string backupId)
        {
            var filePath = GetFilePath(userDirectory, backupId);
            
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
                await stream.CopyToAsync(memory);
            
            memory.Position = 0;
            
            return memory;
        }

        public IEnumerable<RemoteBackup> GetLocalBackupsState(string userDirectory, string backupName)
        {
            if (string.IsNullOrWhiteSpace(userDirectory) || !Directory.Exists(userDirectory))
                return new List<RemoteBackup>();

            var searchPattern = $"{backupName}*.*";
            return Directory.GetFiles(userDirectory, searchPattern)
                .Select(FileToRemoteBackupState)
                .ToList();
        }

        private RemoteBackup FileToRemoteBackupState(string fileName)
        {
            return new RemoteBackup(
                GetIdByFileName(Path.GetFileName(fileName)),
                Path.GetFileName(fileName),
                new FileInfo(fileName).LastWriteTime);
        }

        public Task Delete(string userDirectory, string backupId)
        {
            var filePath = GetFilePath(userDirectory, backupId);
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Cannot delete file: {filePath}");
            }

            return Task.CompletedTask;
        }

        private string GetFilePath(string userDirectory, string backupId)
        {
            return Path.Combine(userDirectory, GetFileNameById(backupId));
        }

        private string GetFileNameById(string backupId)
        {
            if (string.IsNullOrWhiteSpace(backupId))
                throw new ArgumentNullException(nameof(backupId));

            return Encoding.UTF8.GetString(Convert.FromBase64String(backupId));
        }

        private string GetIdByFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (fileName.Contains(Path.DirectorySeparatorChar) || fileName.Contains(Path.AltDirectorySeparatorChar))
                throw new ArgumentException("incorrect filename");

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(fileName));
        }
    }
}