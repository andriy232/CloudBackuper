using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Helper;
using Helper.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using File = Google.Apis.Drive.v3.Data.File;

namespace GDriveProvider
{
    public class GoogleDriveWrapper : ProviderBase<BackupSettings>, IProvider
    {
        public sealed class OAuthWrapper : IDisposable
        {
            // from
            // https://github.com/googlesamples/oauth-apps-for-windows/tree/master/OAuthConsoleApp

            private const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
            private static readonly AutoResetEvent _waitEvent = new AutoResetEvent(false);
            private const string FileName = "oauth_credentials.json";
            private GoogleAuthorizationCodeFlow _flow;
            private HttpListener _http;

            public UserCredential GetCredentialsSync(string scope)
            {
                if (string.IsNullOrWhiteSpace(Get.Auth.ClientId))
                    throw new ArgumentException(nameof(Settings.Auth.ClientId));
                if (string.IsNullOrWhiteSpace(Settings.Auth.ClientSecret))
                    throw new ArgumentException(nameof(Settings.Auth.ClientSecret));

                var tokenFilePath = WaitOAuthTokenLocation(scope);
                return OpenCredentials(scope, tokenFilePath);
            }

            private UserCredential OpenCredentials(string scope, string tokenFilePath)
            {
                JObject p = null;

                try
                {
                    using (var reader = System.IO.File.OpenText(tokenFilePath))
                    {
                        var n = new JsonSerializer();
                        var r = n.Deserialize(reader, typeof(object));
                        var str = r as string;
                        p = JObject.Parse(str);
                    }
                }
                catch (Exception ex)
                {
                    Helper.Core.WriteLine(ex);
                }

                if (p == null)
                {
                    Debug.Assert(false);
                    return null;
                }

                var accessToken = p["access_token"].Value<string>();
                var refreshToken = p["refresh_token"].Value<string>();

                var authCodeFlow = new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = Settings.Auth.ClientId,
                        ClientSecret = Settings.Auth.ClientSecret
                    },
                    Scopes = new[] {scope},
                    DataStore = new FileDataStore("Store")
                };

                UserCredential credential;
                _flow = new GoogleAuthorizationCodeFlow(authCodeFlow);
                {
                    var token = new TokenResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };

                    credential = new UserCredential(_flow, Environment.UserName, token);
                }

                return credential;
            }

            private string WaitOAuthTokenLocation(string scope)
            {
                if (string.IsNullOrWhiteSpace(scope))
                    throw new ApplicationException("scope is null");

                if (ResultExist(scope, out var resultFile))
                    return resultFile;

                Helper.Core.WriteLine("+-----------------------+");
                Helper.Core.WriteLine("|  Sign in with Google  |");
                Helper.Core.WriteLine("+-----------------------+");

                DoOAuth(scope);

                resultFile = WaitForResult(scope);

                Helper.Core.WriteLine("+-----------------------+");
                Helper.Core.WriteLine("|   Sign in complete    |");
                Helper.Core.WriteLine("+-----------------------+");

                return resultFile;
            }

            private static string ResultFilePath(string scope)
            {
                var s = new Uri(scope).Segments;
                var l = s[s.Length - 1];
                var path = Helper.Core.InitAppFolder();
                return Path.Combine(path, $"{l}_{FileName}");
            }

            private static string WaitForResult(string scope)
            {
                _waitEvent.WaitOne();
                return ResultFilePath(scope);
            }

            private static bool ResultExist(string scope, out string path)
            {
                path = ResultFilePath(scope);
                return !string.IsNullOrWhiteSpace(path) && System.IO.File.Exists(path) &&
                       System.IO.File.ReadAllText(path).Length > 1;
            }

            // ref http://stackoverflow.com/a/3978040
            private static int GetRandomUnusedPort()
            {
                var listener = new TcpListener(IPAddress.Loopback, 0);
                listener.Start();
                var port = ((IPEndPoint) listener.LocalEndpoint).Port;
                listener.Stop();
                return port;
            }

            private async void DoOAuth(string scope)
            {
                // Generates state and PKCE values.
                var state = RandomDataBase64Url(32);
                var codeVerifier = RandomDataBase64Url(32);
                var codeChallenge = Base64UrlencodeNoPadding(ToSha256(codeVerifier));
                const string code_challenge_method = "S256";

                // Creates a redirect URI using an available port on the loopback address.
                var redirectURI = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
                Output("redirect URI: " + redirectURI);

                // Creates an HttpListener to listen for requests on that redirect URI.
                _http = new HttpListener();
                _http.Prefixes.Add(redirectURI);
                Output("Listening..");
                _http.Start();

                // Creates the OAuth 2.0 authorization request.
                var authorizationRequest = string.Format(
                    "{0}?response_type=code&scope=openid%20profile%20{1}&redirect_uri={2}&client_id={3}&state={4}&code_challenge={5}&code_challenge_method={6}",
                    AuthorizationEndpoint,
                    scope,
                    Uri.EscapeDataString(redirectURI),
                    Settings.Auth.ClientId,
                    state,
                    codeChallenge,
                    code_challenge_method
                );

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var request = authorizationRequest.Replace("&", "^&");
                    var psi = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = $"/c start {request}"
                    };
                    Process.Start(psi);
                    // Process.Start do not work in .Net Core
                    //System.Diagnostics.Process.Start(authorizationRequest);
                }

                // Waits for the OAuth authorization response.
                var context = await _http.GetContextAsync();

                // Brings the Console to Focus.
                BringConsoleToFront();

                // Sends an HTTP response to the browser.
                var response = context.Response;
                var responseString =
                    "<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>";
                var buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                var responseOutput = response.OutputStream;
                var responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith(task =>
                {
                    responseOutput.Close();
                    _http.Stop();
                    Helper.Core.WriteLine("HTTP server stopped.");
                });

                // Checks for errors.
                if (context.Request.QueryString.Get("error") != null)
                {
                    Output($"OAuth authorization error: {context.Request.QueryString.Get("error")}.");
                    return;
                }

                if (context.Request.QueryString.Get("code") == null
                    || context.Request.QueryString.Get("state") == null)
                {
                    Output("Malformed authorization response. " + context.Request.QueryString);
                    return;
                }

                // extracts the code
                var code = context.Request.QueryString.Get("code");
                var incomingState = context.Request.QueryString.Get("state");

                // Compares the receieved state to the expected value, to ensure that
                // this app made the request which resulted in authorization.
                if (incomingState != state)
                {
                    Output($"Received request with invalid state ({incomingState})");
                    return;
                }

                Output("Authorization code: " + code);

                // Starts the code exchange at the Token Endpoint.
                PerformCodeExchange(code, codeVerifier, redirectURI, scope);
            }

            private async void PerformCodeExchange(string code, string codeVerifier, string redirectURI, string scope)
            {
                Output("Exchanging code for tokens...");

                // builds the  request
                var tokenRequestURI = "https://www.googleapis.com/oauth2/v4/token";
                var tokenRequestBody = string.Format(
                    "code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code",
                    code,
                    Uri.EscapeDataString(redirectURI),
                    Settings.Auth.ClientId,
                    codeVerifier,
                    Settings.Auth.ClientSecret
                );

                // sends the request
                var tokenRequest = (HttpWebRequest) WebRequest.Create(tokenRequestURI);
                tokenRequest.Method = "POST";
                tokenRequest.ContentType = "application/x-www-form-urlencoded";
                tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                var byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
                tokenRequest.ContentLength = byteVersion.Length;

                using (var stream = tokenRequest.GetRequestStream())
                {
                    await stream.WriteAsync(byteVersion, 0, byteVersion.Length);
                    stream.Close();
                }

                try
                {
                    // gets the response
                    var tokenResponse = await tokenRequest.GetResponseAsync();
                    using (var reader = new StreamReader(tokenResponse.GetResponseStream()))
                    {
                        // reads response body
                        var responseText = await reader.ReadToEndAsync();
                        Helper.Core.WriteLine(responseText);
                        FileOutput(responseText, scope);

                        // converts to dictionary
                        var tokenEndpointDecoded =
                            JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);

                        var accessToken = tokenEndpointDecoded["access_token"];
                        UserinfoCall(accessToken);
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (ex.Response is HttpWebResponse response)
                        {
                            Output("HTTP: " + response.StatusCode);
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                // reads response body
                                var responseText = await reader.ReadToEndAsync();
                                Output(responseText);
                            }
                        }
                    }
                }
            }

            private async void UserinfoCall(string accessToken)
            {
                Output("Making API Call to Userinfo...");

                // builds the  request
                var userinfoRequestURI = "https://www.googleapis.com/oauth2/v3/userinfo";

                // sends the request
                var userinfoRequest = (HttpWebRequest) WebRequest.Create(userinfoRequestURI);
                userinfoRequest.Method = "GET";
                userinfoRequest.Headers.Add(string.Format("Authorization: Bearer {0}", accessToken));
                userinfoRequest.ContentType = "application/x-www-form-urlencoded";
                userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                // gets the response
                var userinfoResponse = await userinfoRequest.GetResponseAsync();
                using (var userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream()))
                {
                    // reads response body
                    var userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();
                    Output(userinfoResponseText);
                }
            }

            private void FileOutput(string text, string scope)
            {
                try
                {
                    var path = ResultFilePath(scope);

                    Output($"Writing to path: {path}");

                    using (var file = System.IO.File.CreateText(path))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(file, text);
                    }
                }
                catch (Exception ex)
                {
                    Output(ex.ToString());
                    Debug.Assert(false, $"something wrong! {ex}");
                }

                _waitEvent.Set();
            }

            private static void Output(string output)
            {
                Helper.Core.WriteLine(output);
            }

            private static string RandomDataBase64Url(uint length)
            {
                byte[] bytes;

                using (var rng = new RNGCryptoServiceProvider())
                {
                    bytes = new byte[length];
                    rng.GetBytes(bytes);
                }

                return Base64UrlencodeNoPadding(bytes);
            }

            private static byte[] ToSha256(string inputStirng)
            {
                var bytes = Encoding.ASCII.GetBytes(inputStirng);
                using (var sha256 = new SHA256Managed())
                    return sha256.ComputeHash(bytes);
            }

            private static string Base64UrlencodeNoPadding(byte[] buffer)
            {
                var base64 = Convert.ToBase64String(buffer);

                // Converts base64 to base64url.
                base64 = base64.Replace("+", "-");
                base64 = base64.Replace("/", "_");
                // Strips padding.
                base64 = base64.Replace("=", "");

                return base64;
            }

            private static void BringConsoleToFront()
            {
                SetForegroundWindow(GetConsoleWindow());
            }

            // Hack to bring the Console window to front.
            // ref: http://stackoverflow.com/a/12066376

            [DllImport("kernel32.dll", ExactSpelling = true)]
            private static extern IntPtr GetConsoleWindow();

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool SetForegroundWindow(IntPtr hWnd);

            public void Dispose()
            {
                _waitEvent?.Dispose();
                _flow?.Dispose();
                _http?.Close();
            }
        }

        public Guid Id => Guid.Parse("{3D8C2F96-32C3-44B0-8B4B-7DD4DE3D3AE6}");
        public string Name => "GDriveProvider";

        private const int EntriesPerPage = 100;
        private const int MaxBackupCount = 10;

        public void BackupIfNeed(UserCredential credential, string path)
        {
            try
            {
                var initializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = Name
                };

                using var service = new DriveService(initializer);
                if (CheckIfNeedUpload(service))
                    Upload(service, path);
            }
            catch (Exception ex)
            {
                Core.WriteLine(ex.ToString());
            }
        }

        private void Upload(DriveService service, string zipPath)
        {
            try
            {
                Core.WriteLine($"Uploading data: {zipPath}");

                var fileMetadata = new File
                {
                    Name = Path.GetFileName(zipPath),
                    Parents = new List<string> {GetSettings(Id).DriveFolderId}
                };
                FilesResource.CreateMediaUpload request;
                using (var stream = new FileStream(zipPath, FileMode.Open))
                {
                    request = service.Files.Create(
                        fileMetadata, stream, "application/zip");
                    request.Fields = "id";
                    request.Upload();
                }

                var file = request.ResponseBody;
                Core.WriteLine($"Backup uploaded, file Id: {file.Id}");
            }
            catch (Exception ex)
            {
                Core.WriteLine(ex.ToString());
            }
            finally
            {
                Core.WriteLine("Clean up garbage");
            }
        }


        private bool CheckIfNeedUpload(DriveService service)
        {
            var folderGet = service.Files.Get(GetSettings(Id).DriveFolderId).SetFields();
            var folderResult = folderGet.Execute();

            var listRequest = service.Files.List().SetFields();

            listRequest.PageSize = EntriesPerPage;
            listRequest.Q = $"'{folderResult.Id}' in parents";

            var backups = new List<File>();
            string pageToken;
            Core.WriteLine("Searching for backups");
            do
            {
                var listRequestResult = listRequest.Execute();
                var files = listRequestResult.Files;
                var neededFiles = files
                    .Where(x => x.Parents != null &&
                                x.Parents.Count > 0 &&
                                x.Trashed == false &&
                                x.Parents.Any(p => p.Equals(folderResult.Id, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                if (neededFiles.Count > 0)
                {
                    foreach (var file in neededFiles)
                    {
                        Core.WriteLine("Remote backup found! " +
                                       $"Name: {file.Name}, FileName: {file.OriginalFilename}");
                    }
                }

                backups.AddRange(neededFiles);

                pageToken = listRequestResult.NextPageToken;
            } while (pageToken != null);

            while (backups.Count > MaxBackupCount)
            {
                var orderedList = backups.Where(x => x.ModifiedTime.HasValue).OrderBy(x => x.ModifiedTime);
                var last = orderedList.First();
                service.Files.Delete(last.Id);
                Core.WriteLine($"Removing backup: {last.OriginalFilename}");
                backups.Remove(last);
            }

            foreach (var file in backups)
            {
                if (string.IsNullOrWhiteSpace(file.OriginalFilename) ||
                    file.OriginalFilename.IndexOf(Environment.MachineName, StringComparison.OrdinalIgnoreCase) < 0)
                    continue;

                if (file.CreatedTime.HasValue && file.CreatedTime.Value.Date == DateTime.Today)
                {
                    Core.WriteLine($"Remote backup exists: id:{file.Id}, filename:{file.OriginalFilename}");
                    return false;
                }
            }

            return true;
        }

        public BackupState GetExistingBackups()
        {
            throw new NotImplementedException();
        }

        public void Upload(Backup path)
        {
            throw new NotImplementedException();
        }

        public object GetValues()
        {
            using var service = new OAuthWrapper();
            var credential = service.GetCredentialsSync(DriveService.Scope.Drive);
            GoogleDriveWrapper.BackupIfNeed(credential);
        }
    }
}