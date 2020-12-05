using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using DataGuardian.Server.Impl;
using DataGuardian.Server.Plugins;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using ContentDispositionHeaderValue = Microsoft.Net.Http.Headers.ContentDispositionHeaderValue;
using MediaTypeHeaderValue = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace DataGuardian.Server.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class DataGuardianController : ControllerBase
    {
        private readonly string[] _permittedExtensions = {".zip"};
        private readonly IStorageManager _storageManager;
        private readonly IAuthManager _authManager;
        private const long MaxFileSize = 10L * 1024L * 1024L * 1024L; // 10GB, adjust to your need

        public static int Port { get; private set; }

        public DataGuardianController(IStorageManager manager, IAuthManager authManager)
        {
            _storageManager = manager;
            _authManager = authManager;
        }


        [HttpGet("home")]
        public IActionResult Home()
        {
            Port = HttpContext?.Connection?.LocalPort ?? 0;

            return Ok("DataGuardian server is running");
        }

        [HttpGet("auth")]
        public IActionResult Auth([FromQuery] string computerId)
        {
            try
            {
                return Ok(_authManager.GetUserUid(computerId));
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error in Auth");
                return BadRequest("invalid parameters");
            }
        }

        [DisableFormValueModelBinding]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadBackup([FromQuery] string userId)
        {
            var userDirectory = _authManager.GetUserDirectory(userId);
            if (string.IsNullOrWhiteSpace(userDirectory))
                throw new Exception("Not registered user");

            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                throw new Exception("Not a multipart request");

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType), int.MaxValue);
            var reader = new MultipartReader(boundary, Request.Body);

            // for a single file
            var section = await reader.ReadNextSectionAsync();

            if (section == null)
                throw new Exception("No sections");

            if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
                throw new Exception("No content disposition in multipart defined");

            var fileName = contentDisposition.FileNameStar.ToString();
            if (string.IsNullOrEmpty(fileName))
                fileName = contentDisposition.FileName.ToString();

            if (string.IsNullOrEmpty(fileName))
                throw new Exception("No filename defined.");

            using var fileStream = section.Body;
            await _storageManager.SaveFileToFileSystem(userDirectory, fileName, fileStream);

            return await Task.FromResult(Ok("file uploaded"));
        }

        [DisableFormValueModelBinding]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        [HttpGet("download")]
        public async Task<FileStreamResult> DownloadBackup([FromQuery] string userId, [FromQuery] string backupId)
        {
            var userDirectory = _authManager.GetUserDirectory(userId);

            if (string.IsNullOrWhiteSpace(userDirectory))
                throw new Exception("Not registered user");
            if (string.IsNullOrWhiteSpace(backupId))
                throw new Exception("backup id is empty");

            var stream = await _storageManager.ReadFile(userDirectory, backupId);

            return File(stream, "text/plain");
        }

        [HttpGet("state")]
        public async Task<IActionResult> GetState([FromQuery] string userId)
        {
            var userDirectory = _authManager.GetUserDirectory(userId);

            if (string.IsNullOrWhiteSpace(userDirectory))
                throw new Exception("Not registered user");

            var state = await _storageManager.ReadState(userDirectory);

            return await Task.FromResult(Ok(state));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFile([FromQuery] string userId, [FromQuery] string backupId)
        {
            var userDirectory = _authManager.GetUserDirectory(userId);

            if (string.IsNullOrWhiteSpace(userDirectory))
                throw new Exception("Not registered user");
            if (string.IsNullOrWhiteSpace(backupId))
                throw new Exception("backup id is empty");

            await _storageManager.Delete(userDirectory, backupId);

            return await Task.FromResult(Ok("file deleted"));
        }
    }
}