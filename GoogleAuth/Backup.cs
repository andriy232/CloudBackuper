using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Helper
{
    public class Backup : IDisposable
    {
        public string ResultPath { get; private set; }

        private string _tempDir;

        public Backup(string path, string backupFileName)
        {
            CreateZip(path, backupFileName);
        }

        public void CreateZip(string targetPath, string backupFileName)
        {
            void CreateArchiveEntry(FileInfo fileInfo, string destinationPath, ZipArchive archive)
            {
                var destination = Path.Combine(fileInfo.DirectoryName, fileInfo.Name)
                    .Substring(destinationPath.Length + 1);

                var fileInfoDirectory = fileInfo.Directory?.ToString();
                var sourceFileName = Path.Combine(fileInfoDirectory, fileInfo.Name);

                Debug.Assert(!string.IsNullOrWhiteSpace(fileInfoDirectory), "path not valid!");

                archive.CreateEntryFromFile(sourceFileName, destination);
            }

            if (string.IsNullOrWhiteSpace(targetPath))
                throw new ArgumentException(nameof(targetPath));

            var sourcePath = Environment.ExpandEnvironmentVariables(targetPath);
            if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath) && !Directory.Exists(sourcePath))
                throw new ArgumentException(nameof(sourcePath));

            try
            {
                var dirName = Guid.NewGuid().ToString("N").Substring(0, 20);
                _tempDir = Path.Combine(Path.GetTempPath(), dirName);
                ClearTemp();

                Directory.CreateDirectory(_tempDir);

                var entryName = Path.GetFileName(sourcePath);
                var destinationPath = Path.Combine(_tempDir, entryName);

                Core.WriteLine($"Copying data to temp: {destinationPath}");

                if (FileSystem.IsDirectory(sourcePath))
                {
                    FileSystem.CopyDirectory(sourcePath, destinationPath);
                }
                else
                {
                    FileSystem.CopyFile(sourcePath, destinationPath);
                }

                var zipName = $"{backupFileName}_{DateTime.Today:dd-MM-yyyy}.zip";
                ResultPath = Path.Combine(_tempDir, zipName);
                Core.WriteLine($"Creating archive: {ResultPath}");

                using (var archive = ZipFile.Open(ResultPath, ZipArchiveMode.Create))
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

                Core.WriteLine($"Data archived: {ResultPath}");
            }
            catch (Exception ex)
            {
                ClearTemp();
                Core.WriteLine(ex);
            }
        }

        private void ClearTemp()
        {
            if (!string.IsNullOrWhiteSpace(_tempDir) && Directory.Exists(_tempDir))
                FileSystem.DeleteDirectory(_tempDir);
        }

        public void Dispose()
        {
            try
            {
                ClearTemp();
            }
            catch (Exception ex)
            {
                Core.WriteLine(ex);
            }
        }
    }
}