using System.IO;
using System.IO.Abstractions;

namespace StaticSiteGenerator.Files.FileWriting;

public class OverwritingFileWriter : IFileWriter
{
    private readonly IFileSystem FileSystem;

    public OverwritingFileWriter(IFileSystem fileSystem)
    {
        FileSystem = fileSystem;
    }

    public void WriteFile(string fileName, string contents)
    {
        if (FileSystem.File.Exists(fileName))
        {
            FileSystem.File.Delete(fileName);
        }

        using (var writer = FileSystem.File.CreateText(fileName))
        {
            writer.Write(contents);
        }

    }
}
