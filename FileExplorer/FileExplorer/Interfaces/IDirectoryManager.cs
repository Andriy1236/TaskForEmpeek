using FileExplorer.Models;
using System.Collections.Generic;

namespace FileExplorer.Interfaces
{
    public interface IDirectoryManager
    {
        DirectoryInformation GetAllInfo(string path);
        
    }
}
