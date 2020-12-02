using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataGuardian.Server.Plugins;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
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
        private const long MaxFileSize = 10L * 1024L * 1024L * 1024L; // 10GB, adjust to your need

        public static int Port { get; private set; }

        public DataGuardianController(IStorageManager manager)
        {
            _storageManager = manager;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            Port = HttpContext?.Connection?.LocalPort ?? 0;

            return Ok("DataGuardian server is running");
        }

        [DisableFormValueModelBinding]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        [HttpPost]
        public async Task<IActionResult> ReceiveFile()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                throw new Exception("Not a multipart request");

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType), int.MaxValue);
            var reader = new MultipartReader(boundary, Request.Body);

            // for a single file,
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
            await _storageManager.SaveFileToFileSystem(fileName, fileStream);

            return await Task.FromResult(Ok("file uploaded"));
        }
    }
}