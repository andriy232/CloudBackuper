using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using DataGuardian.LocalServer.Properties;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Plugins;

namespace DataGuardian.LocalServer
{
    public class LocalServer : CloudStorageProviderBase<LocalServerSettings>
    {
        public override Guid Id => Guid.Parse("D67A4F3C-B930-4631-B690-E2EF53C93EE5");

        public override string Description => "DataGuardian Local server allow you to store files at server in your Network";

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
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

                var portsRange = new[] { 4444, 4445, 4446, 4447, 4448 };

                var listener = IPGlobalProperties.GetIPGlobalProperties()
                    .GetActiveTcpListeners()
                    .FirstOrDefault(tcp => tcp.Address.Equals(IPAddress.Any) && portsRange.Contains(tcp.Port));

                if (listener != null)
                {
                    using (var httpClient = new HttpClient())
                    {
                        var address = new UriBuilder(Uri.UriSchemeHttps, IPAddress.Loopback.ToString(), listener.Port)
                        {
                            Path = "dataguardian"
                        };

                        var result = httpClient.GetAsync(address.Uri).GetAwaiter().GetResult();
                        var response = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        if (response == "DataGuardian server is running")
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Log(Name, ex);
            }

            throw new ApplicationException("No running local server");
        }

        public override Task<RemoteBackupsState> GetBackupState(IAccount account, string backupFileName)
        {
            throw new NotImplementedException();
        }

        public override Task UploadBackupAsync(IAccount account, LocalArchivedBackup localBackup)
        {
            throw new NotImplementedException();
        }

        public override Task DownloadBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup,
            string outputPath)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup)
        {
            throw new NotImplementedException();
        }
    }
}