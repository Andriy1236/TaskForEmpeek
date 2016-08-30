using FileExplorer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;


namespace FileExplorer.Models
{
    public class FileManager:IFileManager
    {
        private const int Mb10 = 10485760;
        private const int Mb50 = 52428800;
        private const int Mb100 = 104857600;

        public FileCount GetFilesCount(string path)
        {
            FileCount CountFiles = new FileCount();

            Stack<string> dirs = new Stack<string>();
            dirs.Push(path);

            while (dirs.Count > 0)
            {
                string currentDirPath = dirs.Pop();
                try
                {
                    string[] subDirs = Directory.GetDirectories(currentDirPath);
                    foreach (string subDirPath in subDirs)
                    {
                        dirs.Push(subDirPath);
                    }

                    FileInfo[] files = null;

                    files = new System.IO.DirectoryInfo(currentDirPath).GetFiles();

                    foreach (var filePath in files)
                    {
                        if (filePath.Length < Mb10)
                            CountFiles.LessTenMb++;

                        if (filePath.Length >= Mb10 && filePath.Length <= Mb50)
                            CountFiles.BetweenTenAndFiftyMb++;

                        if (filePath.Length > Mb100)
                            CountFiles.MoreOnehundredMb++;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return CountFiles;
        }


        public FileCount GetCountFilesFromAllDisk(IEnumerable<string> paths)
        {
            FileCount counts = new FileCount();

            foreach (var path in paths)
            {
                FileCount count = GetFilesCount(path);
                counts.LessTenMb += count.LessTenMb;
                counts.BetweenTenAndFiftyMb += count.BetweenTenAndFiftyMb;
                counts.MoreOnehundredMb += count.MoreOnehundredMb;
            }

            return counts;
        }

    }
}