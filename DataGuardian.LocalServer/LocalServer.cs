using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using DataGuardian.LocalServer.Properties;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Plugins;
using Newtonsoft.Json;

namespace DataGuardian.LocalServer
{
    public class LocalServer : CloudStorageProviderBase<LocalServerSettings>
    {
        public override Guid Id => Guid.Parse("D67A4F3C-B930-4631-B690-E2EF53C93EE5");

        public override string Description =>
            "DataGuardian Local server allow you to store files at server in your Network";

        public override byte[] Logo
        {
            get
            {
                using (var stream = new MemoryStream())
                {
                    Resources.Img_Server.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }

        public override string Name => "Local Server";

        public override object TryAuth()
        {
            try
            {
                using (var httpClient = GetClient())
                {
                    var uri = GetUri("auth", $"computerid={EnvironmentHelper.GetUniqueComputerId()}");
                    var result = httpClient.GetAsync(uri).GetAwaiter().GetResult();
                    var response = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    if (Guid.TryParse(response, out _))
                        return new LocalServerSettings {UID = response};
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Log(Name, ex);
            }

            throw new ApplicationException("No running local server");
        }

        private IPEndPoint GetEndpoint()
        {
            var portsRange = new[] {4444, 4445, 4446, 4447, 4448};

            return IPGlobalProperties.GetIPGlobalProperties()
                .GetActiveTcpListeners()
                .FirstOrDefault(tcp => tcp.Address.Equals(IPAddress.Any) && portsRange.Contains(tcp.Port));
        }

        private static void PerformHacks()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
        }

        private HttpClient GetClient()
        {
            PerformHacks();
            return new HttpClient();
        }

        private Uri GetUri(string path, string query = "")
        {
            var endpoint = GetEndpoint();

            var address = new UriBuilder(Uri.UriSchemeHttps, IPAddress.Loopback.ToString(), endpoint.Port)
            {
                Path = "dataguardian/" + path,
                Query = query ?? string.Empty
            };

            return address.Uri;
        }

        private Uri BuildUri(string path, IAccount account, string backupUniqueId = "")
        {
            var query = $"userId={GetSettings(account).UID}";

            if (!string.IsNullOrWhiteSpace(backupUniqueId))
                query += $"&backupId={backupUniqueId}";

            return GetUri(path, query);
        }

        public override async Task<RemoteBackupsState> GetBackupState(IAccount account, string backupFileName)
        {
            using var client = GetClient();

            var uri = BuildUri("state", account);
            var response = await client.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<RemoteBackup>>(content);

            return new RemoteBackupsState(this, backupFileName, list);
        }

        public override async Task UploadBackupAsync(IAccount account, LocalArchivedBackup localBackup)
        {
            using var client = GetClient();
            using var formData = new MultipartFormDataContent();
            var uri = BuildUri("upload", account);

            var fileContent = new ByteArrayContent(File.ReadAllBytes(localBackup.ResultPath));

            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Path.GetFileName(localBackup.ResultPath)
            };

            formData.Add(fileContent);


            var response = await client.PostAsync(uri, formData);

            // using var request = new HttpRequestMessage
            // {
            //     Method = HttpMethod.Post,
            //     RequestUri = uri,
            //     Content = new StreamContent(File.OpenRead(localBackup.ResultPath)),
            // };
            // using var response = await client.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("send failed");
        }

        public override async Task DownloadBackupAsync(IAccount account, RemoteBackup backup,
            string outputPath)
        {
            using var client = GetClient();

            var uri = BuildUri("download", account, backup.UniqueId);

            var response = await client.GetAsync(uri);
            var stream = await response.Content.ReadAsStreamAsync();

            using var fileStream = File.OpenWrite(outputPath);
            await stream.CopyToAsync(fileStream);
        }

        public override async Task DeleteBackupAsync(IAccount account, RemoteBackup backup)
        {
            using var client = GetClient();

            var uri = BuildUri("delete", account, backup.UniqueId);
            var response = await client.DeleteAsync(uri);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException("delete failed");
        }
    }
}