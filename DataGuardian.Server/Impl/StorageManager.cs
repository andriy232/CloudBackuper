using System;
using System.IO;
using System.Threading.Tasks;
using DataGuardian.Server.Models;
using DataGuardian.Server.Plugins;
using Microsoft.Extensions.Options;
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

        public async Task SaveFileToFileSystem(string fileName, Stream stream)
        {
            try
            {
                var directory = _settings?.DataStorageLocation;
                if (string.IsNullOrWhiteSpace(directory))
                    directory = Path.GetTempPath();

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var filePath = Path.Combine(directory, fileName);
                await using var fileStream = System.IO.File.OpenWrite(filePath);

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
    }
}