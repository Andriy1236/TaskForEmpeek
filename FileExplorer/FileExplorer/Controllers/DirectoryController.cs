using FileExplorer.Interfaces;
using FileExplorer.Models;
using System;
using System.Web.Http;


namespace FileExplorer.Controllers
{
    public class DirectoryController : ApiController
    {
        private readonly FileManager fileCountrepository = new FileManager();
        private readonly IDirectoryManager directoryManager = new DirectoryManager();
       
        public Models.DirectoryInformation GetDrivers()
        {
            return new Models.DirectoryInformation()
            {
                Directories = Environment.GetLogicalDrives(),
                Files = null,
                Parent = null
            };
        }

        public Models.DirectoryInformation GetDirectories(string path)
        {
            return directoryManager.GetAllInfo(path);
        }
        [Route("api/directory/filecount")]
        public FileCount GetCountFromAllDisk()
        {
            return fileCountrepository.GetCountFilesFromAllDisk(Environment.GetLogicalDrives());
        }
        [Route("api/directory/filecount")]
        public FileCount GetCountFromDirectory([FromUri] string path)
        {
            return fileCountrepository.GetFilesCount(path);
        }


    }
}
