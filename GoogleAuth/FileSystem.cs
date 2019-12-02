using System;
using System.Collections.Generic;
using System.IO;

namespace Helper
{
    public class FileSystem
    {
        private static FileSystem _instance;

        private FileSystem()
        {
        }

        public static FileSystem GetInstance()
        {
            return _instance ?? (_instance = new FileSystem());
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
    }
}