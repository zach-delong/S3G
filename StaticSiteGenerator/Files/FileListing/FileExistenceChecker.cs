using System.IO.Abstractions;

namespace StaticSiteGenerator.Files.FileListing;

public class FileExistenceChecker
{
    private readonly IFileSystem fileSystem;

    public FileExistenceChecker(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem;
    }

    public virtual bool FileExists(string path)
    {
        System.Console.WriteLine(path);
        return fileSystem.File.Exists(path);
    }
}
