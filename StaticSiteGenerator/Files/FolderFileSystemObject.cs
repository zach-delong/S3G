namespace StaticSiteGenerator.Files;

public class FolderFileSystemObject : IFileSystemObject
{
    public FolderFileSystemObject(string path)
    {
        FullPath = path;
    }
    public string FullPath { get; }
}
