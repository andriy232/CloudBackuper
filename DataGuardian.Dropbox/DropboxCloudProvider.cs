﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DataGuardian.Dropbox.Properties;
using DataGuardian.GUI;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Core;
using DataGuardian.Plugins.Plugins;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace DataGuardian.Dropbox
{
    public class DropboxCloudProvider : CloudStorageProviderBase<DropboxSettings>
    {
        public override string Name => "Dropbox";

        public override byte[] Logo => Resources.Img_Dropbox;

        public override string Description => Resources.Str_DropboxCloudStorageProvider_Description;

        public override Guid Id => Guid.Parse("{D799FFF5-CACC-4E02-ACFD-ED2275F3BE56}");

        // loopback address for login process
        private const string LoopbackHost = "http://127.0.0.1:52475/";

        // also need to register this redirect on https://www.dropbox.com/developers/apps.
        private readonly Uri RedirectUri = new Uri(LoopbackHost + "authorize");

        // url to receive access token
        private readonly Uri JSRedirectUri = new Uri(LoopbackHost + "token");

        // 128KB
        private const int ChunkSize = 128 * 1024;

        private const string RemotePath = "/Backup";

        private async Task<DropboxSettings> GetAuthSettings(IAccount account = null)
        {
            DropboxCertHelper.InitializeCertPinning();

            var accessSettings = account != null ? GetSettings(account) : null;

            if (string.IsNullOrEmpty(accessSettings?.AccessToken))
                accessSettings = await Autorize();

            if (string.IsNullOrEmpty(accessSettings?.AccessToken))
                throw new ArgumentException(nameof(accessSettings));

            return accessSettings;
        }

        /// <summary>
        /// Handles the redirect from Dropbox server.
        /// Because we are using token flow, the local
        /// http server cannot directly receive the URL fragment. We need to return a HTML page with
        /// inline JS which can send URL fragment to local server as URL parameter.
        /// </summary>
        /// <param name="http">The http listener.</param>
        /// <returns>The <see cref="Task"/></returns>
        private async Task HandleOAuth2Redirect(HttpListener http)
        {
            var context = await http.GetContextAsync();

            // We only care about request to RedirectUri endpoint.
            while (context.Request.Url.AbsolutePath != RedirectUri.AbsolutePath)
            {
                context = await http.GetContextAsync();
            }

            context.Response.ContentType = "text/html";

            // Respond with a page which runs JS and sends URL fragment as query string
            // to TokenRedirectUri.
            var path = Directory.EnumerateFiles(
                Directory.GetCurrentDirectory(),
                "index.html",
                SearchOption.AllDirectories);
            var firstOrDefault = path.FirstOrDefault();

            if (firstOrDefault != null)
            {
                using (var file = File.OpenRead(firstOrDefault))
                    file.CopyTo(context.Response.OutputStream);
            }
            else
            {
                Core.Logger.Log("Cannot find 'index.html'");
            }

            context.Response.OutputStream.Close();
        }

        /// <summary>
        /// Handle the redirect from JS and process raw redirect URI with fragment to
        /// complete the authorization flow.
        /// </summary>
        /// <param name="http">The http listener.</param>
        /// <returns>The <see cref="OAuth2Response"/></returns>
        private async Task<OAuth2Response> HandleJSRedirect(HttpListener http)
        {
            var context = await http.GetContextAsync();

            // We only care about request to TokenRedirectUri endpoint.
            while (context.Request.Url.AbsolutePath != JSRedirectUri.AbsolutePath)
            {
                context = await http.GetContextAsync();
            }

            var redirectUri = new Uri(context.Request.QueryString["url_with_fragment"]);

            var result = DropboxOAuth2Helper.ParseTokenFragment(redirectUri);

            return result;
        }

        /// <summary>
        /// Gets the dropbox access token.
        /// (or if the user chooses to reset the settings) then the UI in <see cref="LoginForm"/> is
        /// displayed to authorize the user.
        /// </summary>
        /// <returns>A valid access token or null.</returns>
        private async Task<DropboxSettings> Autorize()
        {
            var apiKey = GuiHelper.ReadLine("Please enter api key");

            try
            {
                Console.WriteLine("Waiting for credentials.");

                var state = Guid.NewGuid().ToString("N");
                var authorizeUri =
                    DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, apiKey, RedirectUri, state: state);
                var http = new HttpListener();
                http.Prefixes.Add(LoopbackHost);

                http.Start();

                GuiHelper.StartProcess(authorizeUri.ToString());

                // send url to local server using JS
                await HandleOAuth2Redirect(http);

                // handle JS redirect
                var result = await HandleJSRedirect(http);

                if (result.State != state)
                {
                    return null;
                }

                Console.WriteLine("and back...");

                Unmanaged.SetForegroundWindow(Unmanaged.GetConsoleWindow());

                var accessToken = result.AccessToken;
                var uid = result.Uid;
                Console.WriteLine("Uid: {0}", uid);

                return new DropboxSettings
                {
                    AccessToken = accessToken,
                    Uid = uid
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return null;
            }
        }

        private async Task<FolderMetadata> CreateFolderAsync(DropboxClient client, string path)
        {
            var lastIndexOf = path.LastIndexOf("/", StringComparison.OrdinalIgnoreCase);
            var basePath = path.Remove(lastIndexOf, path.Length - lastIndexOf);

            var existFolders = await client.Files.ListFolderAsync(basePath);
            var any = existFolders.Entries.FirstOrDefault(x => x.IsFolder && x.PathDisplay.Contains(path));
            if (any != null)
                return (FolderMetadata) any;

            var folderArg = new CreateFolderArg(path);
            var folder = await client.Files.CreateFolderV2Async(folderArg);

            return folder.Metadata;
        }

        public override async Task<RemoteBackupsState> GetBackupState(
            IAccount account,
            string backupFileName)
        {
            using (var client = await GetDropboxClient(account))
            {
                return await ListFolderAsync(client, backupFileName, RemotePath);
            }
        }

        private async Task<RemoteBackupsState> ListFolderAsync(DropboxClient client, string backupFileName, string path)
        {
            var list = await client.Files.ListFolderAsync(path);
            var files = list?.Entries?.Where(i => i.IsFile && i.Name.Contains(backupFileName)).Select(x => x.AsFile).ToList() ?? new List<FileMetadata>();

            while (list != null && list.HasMore)
            {
                list = await client.Files.ListFolderContinueAsync(list.Cursor);

                var filesMetadata = list.Entries.Where(m => m.IsFile && m.Name.Contains(backupFileName))
                    .Select(x => x.AsFile);

                files.AddRange(filesMetadata);
            }

            var filtered = files.Select(x => (x.AsFile.Id, x.Name, x.AsFile.ClientModified));
            return new RemoteBackupsState(this, filtered);
        }

        public override async Task UploadBackupAsync(IAccount account, LocalArchivedBackup localBackup)
        {
            using (var client = await GetDropboxClient(account))
            {
                await CreateFolderAsync(client, RemotePath);

                var size = FileSystem.GetFileSize(localBackup.ResultPath);
                if (size > ChunkSize)
                {
                    await UploadChunkedAsync(client, RemotePath, localBackup.ResultPath);
                }
                else
                {
                    await UploadAsync(client, RemotePath, localBackup.ResultPath);
                }
            }
        }

        public override async Task DownloadBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup, string outputPath)
        {
            using (var client = await GetDropboxClient(account))
            {
                var list = await client.Files.ListFolderAsync(RemotePath);

                var file = list.Entries.Select(x => x.AsFile)
                    .Where(x => x != null)
                    .FirstOrDefault(x => x.AsFile.Id == backup.UniqueId);

                if (file != null)
                    await DownloadToFileAsync(client, RemotePath, file, outputPath);
                else
                    Core.Logger.Log("Remote file not found");
            }
        }

        private async Task DownloadToFileAsync(DropboxClient client, string folder, FileMetadata fileMetadata,
            string outputPath)
        {
            using (var response = await client.Files.DownloadAsync($"{folder}/{fileMetadata.Name}"))
            {
                Console.WriteLine($"Downloaded {response.Response.Name} Rev {response.Response.Rev}");
                try
                {
                    using (var stream = File.OpenWrite(outputPath))
                    {
                        var dataToWrite = await response.GetContentAsByteArrayAsync();
                        stream.Write(dataToWrite, 0, dataToWrite.Length);
                    }
                }
                catch (Exception ex)
                {
                    Core.Logger.Log(ex);
                }
            }
        }

        public override async Task DeleteBackupAsync(IAccount account, RemoteBackupsState.RemoteBackup backup)
        {
            using (var client = await GetDropboxClient(account))
            {
                var list = await client.Files.ListFolderAsync(RemotePath);

                var fileMetadata = list.Entries.Select(x => x.AsFile)
                    .Where(x => x != null)
                    .FirstOrDefault(x => x.AsFile.Id == backup.UniqueId);

                if (fileMetadata != null)
                    await client.Files.DeleteV2Async($"{RemotePath}/{fileMetadata.Name}");
                else
                    Core.Logger.Log("Remote file not found");
            }
        }

        private async Task<DropboxClient> GetDropboxClient(IAccount account)
        {
            var accessToken = await GetAuthSettings(account);

            var config = new DropboxClientConfig(Name)
            {
                HttpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromDays(1)
                }
            };

            return new DropboxClient(accessToken.AccessToken, config);
        }

        private async Task UploadChunkedAsync(DropboxClient client, string folder, string fileName)
        {
            var content = File.ReadAllBytes(fileName);

            using (var stream = new MemoryStream(content))
            {
                var numChunks = (int) Math.Ceiling((double) stream.Length / ChunkSize);

                var buffer = new byte[ChunkSize];
                string sessionId = null;

                for (var idx = 0; idx < numChunks; idx++)
                {
                    var byteRead = stream.Read(buffer, 0, ChunkSize);

                    using (var memStream = new MemoryStream(buffer, 0, byteRead))
                    {
                        if (idx == 0)
                        {
                            var result = await client.Files.UploadSessionStartAsync(body: memStream);
                            sessionId = result.SessionId;
                        }
                        else
                        {
                            var cursor = new UploadSessionCursor(sessionId, (ulong) (ChunkSize * idx));
                            if (idx == numChunks - 1)
                            {
                                await client.Files.UploadSessionFinishAsync(
                                    cursor,
                                    new CommitInfo(folder + "/" + Path.GetFileName(fileName)),
                                    memStream);
                            }
                            else
                            {
                                await client.Files.UploadSessionAppendV2Async(cursor, body: memStream);
                            }
                        }
                    }
                }
            }
        }

        private async Task UploadAsync(DropboxClient client, string folder, string fileName)
        {
            var fileContent = File.ReadAllBytes(fileName);
            var resultPath = $"{folder}/{Path.GetFileName(fileName)}";

            using (var stream = new MemoryStream(fileContent))
            {
                var response = await client.Files.UploadAsync(resultPath,
                    WriteMode.Overwrite.Instance,
                    body: stream);

                Core.Logger.Log($"Uploaded Id {response.Id} Rev {response.Rev}");
            }
        }

        public override object TryAuth()
        {
            var task = Task.Run(() => GetAuthSettings());

            task.Wait();

            return task.Result;
        }
    }
}