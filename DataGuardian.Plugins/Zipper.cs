using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using DataGuardian.Plugins.Core;

namespace DataGuardian.Plugins
{
    public static class Zipper
    {
        public static async Task Unzip(string zipPath, string targetPath)
        {
            ZipFile.ExtractToDirectory(zipPath, targetPath);
        }

        static void CreateArchiveEntry(FileInfo fileInfo, string destinationPath, ZipArchive archive)
        {
            var destination = Path.Combine(fileInfo.DirectoryName, fileInfo.Name)
                .Substring(destinationPath.Length + 1);

            var fileInfoDirectory = fileInfo.Directory?.ToString();
            var sourceFileName = Path.Combine(fileInfoDirectory, fileInfo.Name);

            Debug.Assert(!string.IsNullOrWhiteSpace(fileInfoDirectory), "path not valid!");

            archive.CreateEntryFromFile(sourceFileName, destination);
        }

        public static void CreateZip(string zipPath, string destinationPath)
        {
            using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                if (FileSystem.IsDirectory(destinationPath))
                {
                    var fileList = FileSystem.GetFilesRecursive(destinationPath);

                    foreach (var fileInfo in fileList)
                    {
                        CreateArchiveEntry(fileInfo, destinationPath, archive);
                    }
                }
                else
                {
                    var fileInfo = new FileInfo(destinationPath);

                    CreateArchiveEntry(fileInfo, Directory.GetParent(destinationPath).FullName, archive);
                }
            }
        }
    }
}