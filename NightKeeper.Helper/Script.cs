using NightKeeper.Helper.Backups;
using NightKeeper.Helper.Settings;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using NightKeeper.Helper.Core;

namespace NightKeeper.Helper
{
    public class Script
    {
        public PeriodicitySettings Period { get; set; }
        public IConnection Connection { get; set; }
        public string TargetPath { get; set; }

        public string BackupFileName { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        public Script()
        {
        }

        public Script(int id,
            IConnection connection,
            string name,
            string targetPath,
            PeriodicitySettings period,
            string backupFileName = "Backup")
        {
            Id = id;
            Connection = connection ?? throw new ArgumentException(nameof(connection));
            TargetPath = targetPath ?? throw new ArgumentException(nameof(targetPath));
            Period = period;
            BackupFileName = backupFileName;
            Name = name ?? throw new ArgumentException(nameof(name));
        }

        public async Task DoBackup()
        {
            if (!Core.Core.Instance.FileSystem.Exists(TargetPath))
                throw new ApplicationException("Target path not exists");

            using var backup = new LocalArchivedBackup(TargetPath, BackupFileName);
            await Connection.Upload(backup);
        }

        public async Task DoRestore(RemoteBackupsState.RemoteBackup lastBackup)
        {
            if (Core.Core.Instance.FileSystem.Exists(TargetPath))
                throw new ApplicationException("Target path already exists");

            var zipPath = Core.Core.Instance.FileSystem.GetTempFilePath(".zip");
            await Connection.StorageProvider.DownloadBackupAsync(lastBackup, zipPath);

            await Zipper.Unzip(zipPath, TargetPath);
        }

        public override string ToString()
        {
            return $"Script: {TargetPath}, {Connection}, {Period}";
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }

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