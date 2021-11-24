namespace StaticSiteGenerator.Files
{
    public class File : IFileSystemObject
    {
        public File(string name) 
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}