namespace StaticSiteGenerator.Files
{
    public class FileFileSystemObject : IFileSystemObject
    {
        public FileFileSystemObject(string name)
        {
            FullPath = name;
        }

        public string FullPath { get; }
    }
}
