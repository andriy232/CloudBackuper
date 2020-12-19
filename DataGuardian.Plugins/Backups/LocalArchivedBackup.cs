using System;
using System.IO;
using DataGuardian.Plugins.Core;

namespace DataGuardian.Plugins.Backups
{
    public class LocalArchivedBackup : ILocalBackup
    {
        public string ResultPath { get; private set; }

        private readonly ICore _core;
        private string _tempDir;

        public LocalArchivedBackup(string path, string backupFileName)
        {
            _core = CoreStatic.Instance;

            CreateArchive(path, backupFileName);
        }

        public void CreateArchive(string targetPath, string backupFileName)
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

                Trace($"Copying data of [{backupFileName}] to temp: {destinationPath}");

                Directory.CreateDirectory(destinationPath);

                if (FileSystem.IsDirectory(sourcePath))
                {
                    FileSystem.CopyDirectory(sourcePath, destinationPath);
                }
                else
                {
                    FileSystem.CopyFile(sourcePath, destinationPath);
                }

                var zipName = $"{backupFileName}_{DateTime.Today:dd-MM-yyyy}_{DateTime.Now:HH-mm-ss}.zip";
                ResultPath = Path.Combine(_tempDir, zipName);

                Trace($"Creating archive: {ResultPath}");

                Zipper.CreateZip(ResultPath, destinationPath);

                Trace($"Data archived: {ResultPath}");
            }
            catch (Exception ex)
            {
                ClearTemp();
                _core.Logger.Log(ex);
            }
        }

        private void Trace(string message)
        {
            _core.Logger.Log(InfoLogLevel.Debug, nameof(CreateArchive), message);
        }

        private void ClearTemp()
        {
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
                _core.Logger.Log(ex);
            }
        }
    }
}