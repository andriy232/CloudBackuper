using NightKeeper.Helper.Core;
using System;
using System.IO;

namespace NightKeeper.Helper.Backups
{
    public class LocalArchivedBackup : IDisposable
    {
        public string ResultPath { get; private set; }

        private readonly Core.Core _core;
        private string _tempDir;

        public LocalArchivedBackup(string path, string backupFileName)
        {
            _core = Core.Core.Instance;

            CreateArchive(path, backupFileName);
        }

        private void CreateArchive(string targetPath, string backupFileName)
        {
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

                _core.Logger.Log($"Copying data to temp: {destinationPath}");
                Directory.CreateDirectory(destinationPath);

                if (FileSystem.IsDirectory(sourcePath))
                {
                    _core.FileSystem.CopyDirectory(sourcePath, destinationPath);
                }
                else
                {
                    _core.FileSystem.CopyFile(sourcePath, destinationPath);
                }

                var zipName = $"{backupFileName}_{DateTime.Today:dd-MM-yyyy}_{DateTime.Now:HH-mm-ss}.zip";
                ResultPath = Path.Combine(_tempDir, zipName);

                _core.Logger.Log($"Creating archive: {ResultPath}");

                Zipper.CreateZip(ResultPath, destinationPath);

                _core.Logger.Log($"Data archived: {ResultPath}");
            }
            catch (Exception ex)
            {
                ClearTemp();
                _core.Logger.Log(ex);
            }
        }

        private void ClearTemp()
        {
            _core.FileSystem.DeleteDirectory(_tempDir);
        }

        public void Dispose()
        {
            try
            {
                ClearTemp();
            }
            catch (Exception ex)
            {
                _core.Logger.Log(ex);
            }
        }
    }
}