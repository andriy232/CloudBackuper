using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DataGuardian.GoogleDrive.Properties;
using DataGuardian.GUI;
using DataGuardian.Plugins;
using DataGuardian.Plugins.Backups;
using DataGuardian.Plugins.Core;
using DataGuardian.Plugins.Plugins;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;

namespace DataGuardian.GoogleDrive
{
    public class GoogleDriveCloudProvider : CloudStorageProviderBase<GoogleDriveSettings>
    {
        public override Guid Id => Guid.Parse("3D8C2F96-32C3-44B0-8B4B-7DD4DE3D3AE6");

        public override string Name => "Google Drive";

        public override string Description => Resources.Str_GoogleDriveProvider_Description;
        
        public override byte[] Logo => Resources.Img_GoogleDrive;

        private static readonly string TargetScope = DriveService.Scope.Drive;
        private const int EntriesPerPage = 100;

        private async Task Upload(IAccount account, DriveService service, string zipPath)
        {
            try
            {
                var fileMetadata = new File
                {
                    Name = Path.GetFileName(zipPath),
                    Parents = new List<string> { GetSettings(account).DriveFolderId }
                };

                FilesResource.CreateMediaUpload request;

                using (var stream = new FileStream(zipPath, FileMode.Open))
                {
                    request = service.Files.Create(
                        fileMetadata, stream, "application/zip");
                    request.Fields = "id";
                    await request.UploadAsync();
                }

                var file = request.ResponseBody;

                Trace(nameof(Upload), $"Backup uploaded, file Id: {file?.Id}");
            }
            catch (Exception ex)
            {
                Log(Name, ex.ToString());
            }
        }


        private async Task Delete(DriveService service, string backupUniqueId)
        {
            var request = service.Files.Delete(backupUniqueId);
            var result = await request.ExecuteAsync();
            Log(Name, $"Removing backup: {result}");
        }

        private async Task<RemoteBackupsState> GetBackupsAsync(IAccount account, string backupFileName, DriveService service)
        {
            var settings = GetSettings(account);
            var folderGet = service.Files.Get(settings.DriveFolderId).SetFields();
            var folderResult = await folderGet.ExecuteAsync();

            var listRequest = service.Files.List().SetFields();

            listRequest.PageSize = EntriesPerPage;
            listRequest.Q = $"'{folderResult.Id}' in parents";

            var backups = new List<File>();
            string pageToken;

            do
            {
                var listRequestResult = await listRequest.ExecuteAsync();
                var files = listRequestResult.Files;
                var neededFiles = files
                    .Where(x => x.Parents != null &&
                                x.Parents.Count > 0 &&
                                x.Trashed == false &&
                                x.OriginalFilename.Contains(backupFileName) &&
                                x.Parents.Any(p => p.Equals(folderResult.Id, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                backups.AddRange(neededFiles);

                pageToken = listRequestResult.NextPageToken;
            } while (pageToken != null);

            var tuples = backups.Select(x => (x.Id, x.Name,
                x.ModifiedTime ?? x.ModifiedByMeTime ?? x.CreatedTime ?? DateTime.MinValue));

            return new RemoteBackupsState(this, backupFileName, tuples);
        }

        public override async Task<RemoteBackupsState> GetBackupState(IAccount account, string backupFileName)
        {
            using (var service = await GetDriveServiceClient(account))
                return await GetBackupsAsync(account, backupFileName, service);
        }

        public override async Task UploadBackupAsync(IAccount account, LocalArchivedBackup localBackup)
        {
            using (var service = await GetDriveServiceClient(account))
                await Upload(account, service, localBackup.ResultPath);
        }

        public override async Task DownloadBackupAsync(IAccount account, RemoteBackup backup, string outputPath)
        {
            using (var service = await GetDriveServiceClient(account))
                await DownloadToFile(service, backup.UniqueId, outputPath);
        }

        private async Task DownloadToFile(DriveService service, string fileId, string outputPath)
        {
            var file = service.Files.Get(fileId);
            using (var stream = System.IO.File.Create(outputPath))
                await file.DownloadAsync(stream);
        }

        public override async Task DeleteBackupAsync(IAccount account, RemoteBackup backup)
        {
            using (var service = await GetDriveServiceClient(account))
                await Delete(service, backup.UniqueId);
        }

        private async Task<DriveService> GetDriveServiceClient(IAccount account)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            var credential = await GetAuthCredentialsAsync(account);
            var initializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = Name
            };

            return new DriveService(initializer);
        }

        private async Task<UserCredential> GetAuthCredentialsAsync(IAccount account)
        {
            return await GetCredentialsStateAsync(null, account)
                .ContinueWith(x =>
                {
                    var valueTuple = x.GetAwaiter().GetResult();
                    return valueTuple.Item1;
                })
                .ConfigureAwait(false);
        }

        private async Task<GoogleDriveSettings> GetOAuthSettingsAsync(string userCredentialsPath)
        {
            // no account yet
            return await GetCredentialsStateAsync(userCredentialsPath, null)
                .ContinueWith(x =>
                {
                    var valueTuple = x.GetAwaiter().GetResult();
                    return valueTuple.Item2;
                }).ConfigureAwait(true);
        }

        private async Task<(UserCredential, GoogleDriveSettings)> GetCredentialsStateAsync(string userCredentialsPath, IAccount account)
        {
            var driveSettings = GetSettings(account);

            if (!FileSystem.Exists(driveSettings?.AuthInfoPath))
            {
                var authDataDir = Path.Combine(Core.Settings.DataDirectory, "Auth");

                var credentialsDir = Path.Combine(authDataDir, "Credentials");
                if (!Directory.Exists(credentialsDir))
                    Directory.CreateDirectory(credentialsDir);

                var authInfoDir = Path.Combine(authDataDir, "AuthInfo");
                if (!Directory.Exists(authInfoDir))
                    Directory.CreateDirectory(authInfoDir);

                var movedCredentialsPath = Path.Combine(credentialsDir, "credentials.json");
                FileSystem.CopyFile(userCredentialsPath, movedCredentialsPath);

                return await DoGoogleOAuth(account, movedCredentialsPath, authInfoDir, TargetScope);
            }

            return await DoGoogleOAuth(account, driveSettings.CredentialsPath, driveSettings.AuthInfoPath, TargetScope);
        }

        private async Task<(UserCredential, GoogleDriveSettings)> DoGoogleOAuth(IAccount account,
            string credentialsPath,
            string authDataDir,
            string scope)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(credentialsPath), "credentials path is empty");
            Debug.Assert(!string.IsNullOrWhiteSpace(authDataDir), "auth data path is empty");

            var credential = HandleGoogleAuthorizationBroker(credentialsPath, authDataDir, scope);

            var settings = GetSettings(account);
            if (settings != null
                && settings.AuthInfoPath.Equals(authDataDir, StringComparison.OrdinalIgnoreCase)
                && settings.CredentialsPath.Equals(credentialsPath, StringComparison.OrdinalIgnoreCase))
                return (credential, settings);

            if (settings == null)
                settings = new GoogleDriveSettings();
            settings.CredentialsPath = credentialsPath;
            settings.AuthInfoPath = authDataDir;

            if (string.IsNullOrWhiteSpace(settings.DriveFolderId))
                settings.DriveFolderId = GuiHelper.ReadLine("Enter folder id (url) in your Google Drive (https://drive.google.com/drive/my-drive) :");

            return (credential, settings);
        }

        private UserCredential HandleGoogleAuthorizationBroker(string credentialsPath, string authDataDir,
            string scope)
        {
            while (true)
            {
                try
                {
                    using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
                    {
                        var cred = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.Load(stream).Secrets,
                            new[] { scope },
                            "user",
                            CancellationToken.None,
                            new FileDataStore(authDataDir, true)).GetAwaiter().GetResult();
                        return cred;
                    }
                }
                catch (Exception ex)
                {
                    Core.Logger.Log($"Authorization issue: {ex},{Environment.NewLine}Please, try again");
                }
            }
        }

        public override object TryAuth()
        {
            var userCredentialsPath = GuiHelper.ReadPath(
                "Enter path to credentials.Json (https://drive.google.com/drive/my-drive):");
            if (!FileSystem.Exists(userCredentialsPath))
                throw new ApplicationException(nameof(userCredentialsPath));

            var task = Task.Run(()=>GetOAuthSettingsAsync(userCredentialsPath));

            task.Wait();

            return task.Result;
        }
    }
}