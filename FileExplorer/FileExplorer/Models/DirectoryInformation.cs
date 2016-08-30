using System.Collections.Generic;

namespace FileExplorer.Models
{
    public class DirectoryInformation
    {
        public DirectoryInformation()
        {
            Directories = new List<string>();
            Files = new List<string>();
        }
        public IEnumerable<string> Directories { get; set; }
        public IEnumerable<string> Files { get; set; }
        public string Parent { get; set; }
        public FileCount CountFiles { get; set; }

       
    }
}