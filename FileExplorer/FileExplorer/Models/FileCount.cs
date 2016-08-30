namespace FileExplorer.Models
{
    public class FileCount
    {
        public int LessTenMb { get; set; }
        public int BetweenTenAndFiftyMb { get; set; }
        public int MoreOnehundredMb { get; set; }
    }
}