using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataGuardian.GoogleDrive.Properties;
using DataGuardian.Helper;
using DataGuardian.Helper.Backups;
using DataGuardian.Helper.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;

namespace DataGuardian.GoogleDrive
{
    public class GoogleDriveProvider : StorageProviderBase<GoogleDriveSettings>
    {
        public override Guid Id => Guid.Parse("3D8C2F96-32C3-44B0-8B4B-7DD4DE3D3AE6");

        public override string Name => "Google Drive";

        public override byte[] Logo => Resources.Img_GoogleDrive;

        private static readonly string TargetScope = DriveService.Scope.Drive;
        private const int EntriesPerPage = 100;

        private async Task Upload(DriveService service, string zipPath)
        {
            try
            {
                var fileMetadata = new File
                {
                    Name = Path.GetFileName(zipPath),
                    Parents = new List<string> { GetSettings().DriveFolderId }
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
                Core.Logger.Log($"Backup uploaded, file Id: {file.Id}");
            }
            catch (Exception ex)
            {
                Core.Logger.Log(ex.ToString());
            }
            finally
            {
                Core.Logger.Log("Clean up garbage");
            }
        }


        private async Task Delete(DriveService service, string backupUniqueId)
        {
            var request = service.Files.Delete(backupUniqueId);
            var result = await request.ExecuteAsync();
            Core.Logger.Log($"Removing backup: {result}");
        }

        private async Task<RemoteBackupsState> GetBackupsAsync(DriveService service)
        {
            var settings = GetSettings();
            var folderGet = service.Files.Get(settings.DriveFolderId).SetFields();
            var folderResult = await folderGet.ExecuteAsync();

            var listRequest = service.Files.List().SetFields();

            listRequest.PageSize = EntriesPerPage;
            listRequest.Q = $"'{folderResult.Id}' in parents";

            var backups = new List<File>();
            string pageToken;
            Core.Logger.Log("Searching for backups");
            do
            {
                var listRequestResult = await listRequest.ExecuteAsync();
                var files = listRequestResult.Files;
                var neededFiles = files
                    .Where(x => x.Parents != null &&
                                x.Parents.Count > 0 &&
                                x.Trashed == false &&
                                x.Parents.Any(p => p.Equals(folderResult.Id, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                backups.AddRange(neededFiles);

                pageToken = listRequestResult.NextPageToken;
            } while (pageToken != null);

            return new RemoteBackupsState(this,
                backups.Select(x => (x.Id, x.Name,
                    x.ModifiedTime ?? x.ModifiedByMeTime ?? x.CreatedTime ?? DateTime.MinValue)));
        }

        public override async Task<RemoteBackupsState> GetBackupState()
        {
            using (var service = await GetDriveService())
                return await GetBackupsAsync(service);
        }

        public override async Task UploadBackupAsync(LocalArchivedBackup localBackup)
        {
            using (var service = await GetDriveService())
                await Upload(service, localBackup.ResultPath);
        }

        public override async Task DownloadBackupAsync(RemoteBackupsState.RemoteBackup backup, string outputPath)
        {
            using (var service = await GetDriveService())
                await DownloadToFile(service, backup.UniqueId, outputPath);
        }

        private async Task DownloadToFile(DriveService service, string fileId, string outputPath)
        {
            var file = service.Files.Get(fileId);
            using (var stream = System.IO.File.Create(outputPath))
                await file.DownloadAsync(stream);
        }

        public override async Task DeleteBackupAsync(RemoteBackupsState.RemoteBackup backup)
        {
            using (var service = await GetDriveService())
                await Delete(service, backup.UniqueId);
        }

        private async Task<DriveService> GetDriveService()
        {
            var credential = await GetAuthCredentialsAsync();
            var initializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = Name
            };

            return new DriveService(initializer);
        }

        private async Task<UserCredential> GetAuthCredentialsAsync()
        {
            return await GetCredentials()
                .ContinueWith(x =>
                {
                    var valueTuple = x.GetAwaiter().GetResult();
                    return valueTuple.Item1;
                })
                .ConfigureAwait(false);
        }

        private async Task<GoogleDriveSettings> GetOAuthSettingsAsync()
        {
            return await GetCredentials()
                .ContinueWith(x =>
                {
                    var valueTuple = x.GetAwaiter().GetResult();
                    return valueTuple.Item2;
                })
                .ConfigureAwait(false);
        }

        private async Task<(UserCredential, GoogleDriveSettings)> GetCredentials()
        {
            var driveSettings = GetSettings();

            if (!Core.FileSystem.Exists(driveSettings?.AuthInfoPath))
            {
                var userCredentialsPath = Core.ReadLine(
                    "Enter path to credentials Json:",
                    str => System.IO.File.Exists(str));

                var authDataDir = Path.Combine(Core.GetAppDataPath(), "Auth");

                var credentialsDir = Path.Combine(authDataDir, "Credentials");
                if (!Directory.Exists(credentialsDir))
                    Directory.CreateDirectory(credentialsDir);

                var authInfoDir = Path.Combine(authDataDir, "AuthInfo");
                if (!Directory.Exists(authInfoDir))
                    Directory.CreateDirectory(authInfoDir);

                var movedCredentialsPath = Path.Combine(credentialsDir, "credentials.json");
                Core.FileSystem.CopyFile(userCredentialsPath, movedCredentialsPath);

                return await DoOAuthGoogle(movedCredentialsPath, authInfoDir, TargetScope);
            }

            return await DoOAuthGoogle(driveSettings.CredentialsPath, driveSettings.AuthInfoPath, TargetScope);
        }

        private async Task<(UserCredential, GoogleDriveSettings)> DoOAuthGoogle(
            string credentialsPath,
            string authDataDir,
            string scope)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(credentialsPath), "credentials path is empty");
            Debug.Assert(!string.IsNullOrWhiteSpace(authDataDir), "auth data path is empty");

            var credential = HandleGoogleAuthorizationBroker(credentialsPath, authDataDir, scope);

            var settings = GetSettings();
            if (settings != null
                && settings.AuthInfoPath.Equals(authDataDir, StringComparison.OrdinalIgnoreCase)
                && settings.CredentialsPath.Equals(credentialsPath, StringComparison.OrdinalIgnoreCase))
                return (credential, settings);

            if (settings == null)
                settings = new GoogleDriveSettings();
            settings.CredentialsPath = credentialsPath;
            settings.AuthInfoPath = authDataDir;
            if (string.IsNullOrWhiteSpace(settings.DriveFolderId))
                settings.DriveFolderId = Core.ReadLine("Enter folder id");

            Core.Settings.SaveSettings(Id, settings);

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
                        Core.Logger.Log($"Credential file saved to: {authDataDir}");
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
            var task = Task.Run(GetOAuthSettingsAsync);

            task.Wait();

            return task.Result;
        }
    }
}