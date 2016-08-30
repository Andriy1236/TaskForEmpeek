using FileExplorer.Models;
using System.Collections.Generic;

namespace FileExplorer.Interfaces
{
    public interface IFileManager
    {
        FileCount GetCountFilesFromAllDisk(IEnumerable<string> paths);
        FileCount GetFilesCount(string path);
    }
}
