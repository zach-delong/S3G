namespace StaticSiteGenerator.Files
{
    public class Folder: IFileSystemObject
    {
        public Folder(string name) 
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}