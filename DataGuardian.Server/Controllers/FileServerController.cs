using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataGuardian.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileServerController : ControllerBase
    {
        private readonly string[] _permittedExtensions = {".zip"};
        private const long MaxFileSize = 10L * 1024L * 1024L * 1024L; // 10GB, adjust to your need

        [DisableFormValueModelBinding]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        public async Task ReceiveFile()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                throw new Exception("Not a multipart request");

            var boundary =
                MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), int.MaxValue);
            var reader = new MultipartReader(boundary, Request.Body);

            // note: this is for a single file, you could also process multiple files
            var section = await reader.ReadNextSectionAsync();

            if (section == null)
                throw new Exception("No sections in multipart defined");

            if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
                throw new Exception("No content disposition in multipart defined");

            var fileName = contentDisposition.FileNameStar.ToString();
            if (string.IsNullOrEmpty(fileName))
                fileName = contentDisposition.FileName.ToString();

            if (string.IsNullOrEmpty(fileName))
                throw new Exception("No filename defined.");

            using var fileStream = section.Body;
            await SendFileSomewhere(fileName, fileStream);
        }

        // This should probably not be inside the controller class
        private async Task SendFileSomewhere(string fileName, Stream stream)
        {
            try
            {
                var filePath = Path.Combine(Path.GetTempPath(), fileName);
                var fileStream = System.IO.File.OpenWrite(filePath);
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(fileStream);
                stream.Close();
            }
            catch (Exception ex)
            {

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