using System;
using System.Collections.Generic;
using System.IO;

namespace DataGuardian.Plugins.Core
{
    public class FileSystem
    {
        public static bool IsValidPath(string path)
        {
            return !string.IsNullOrEmpty(path) && (Directory.Exists(path) || File.Exists(path));
        }

        public static IEnumerable<FileInfo> GetFilesRecursive(string path, List<FileInfo> currentData = null)
        {
            if (currentData == null)
                currentData = new List<FileInfo>();

            var directory = new DirectoryInfo(path);

            foreach (var file in directory.GetFiles())
                currentData.Add(file);

            foreach (var d in directory.GetDirectories()) GetFilesRecursive(d.FullName, currentData);
            return currentData;
        }

        public static void CopyDirectory(string sourcePath, string destinationPath)
        {
            foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

            foreach (var newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                CopyFile(newPath, newPath.Replace(sourcePath, destinationPath));
        }

        public static void CopyFile(string sourcePath, string destinationPath)
        {
            try
            {
                File.Copy(sourcePath, destinationPath, true);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static bool IsDirectory(string path)
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }

        public static void DeleteDirectory(string dir)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(dir) && Directory.Exists(dir))
                    Directory.Delete(dir, true);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static long GetFileSize(string file)
        {
            return new FileInfo(file).Length;
        }

        public static bool Exists(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            return Path.HasExtension(filePath) && File.Exists(filePath) || Directory.Exists(filePath);
        }

        public void Delete(string targetPath)
        {
            if (string.IsNullOrWhiteSpace(targetPath))
                return;

            if (File.Exists(targetPath))
                File.Delete(targetPath);
            else if (Directory.Exists(targetPath))
                Directory.Delete(targetPath, true);
        }

        public static string GetTempFilePath(string extension)
        {
            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            if (string.IsNullOrEmpty(extension))
                return tempFile;
            if (extension.Contains("."))
                return tempFile + extension;
            return tempFile + "." + extension;
        }

        public static void CreateDirectoryIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public static IEnumerable<string> GetAllFiles(string directory)
        {
            return Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
        }
    }
}