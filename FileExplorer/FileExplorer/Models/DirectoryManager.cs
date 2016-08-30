using FileExplorer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileExplorer.Models
{
    public class DirectoryManager : IDirectoryManager
    {
        private IEnumerable<string> GetDirectories(string path)
        {
            IEnumerable<string> Directories = null;
            try
            {
                Directories = Directory.GetDirectories(path).Select(x => new System.IO.DirectoryInfo(x).Name);
            }

            catch (Exception e)
            {
            }
            return Directories;


        }

        private IEnumerable<string> GetFiles(string path)
        {
            IEnumerable<string> Files = null;
            try
            {
                Files = Directory.GetFiles(path).Select(x => new FileInfo(x).Name);
            }
            catch (Exception e)
            {
            }
            return Files;
        }

        private string GetDirectoryParent(string path)
        {
            string parentDir = null;
            try
            {
                parentDir = Directory.GetParent(path).FullName;
            }
            catch (Exception e)
            {
            }

            return parentDir != null ? parentDir : "";
        }

        public DirectoryInformation GetAllInfo(string path)
        {
            return new Models.DirectoryInformation()
            {
                Directories = this.GetDirectories(path),
                Files = this.GetFiles(path),
                Parent = this.GetDirectoryParent(path)
            };
        }
    }
}