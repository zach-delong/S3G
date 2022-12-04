using System.IO.Abstractions;

namespace StaticSiteGenerator.Utilities;

public class LinkProcessor: ILinkProcessor
{
    private readonly FilePathValidator FilePathValidator;
    private readonly IFileSystem FileSystem;

    public LinkProcessor(FilePathValidator filePathValidator, IFileSystem fileSystem)
    {
        FilePathValidator = filePathValidator;
        FileSystem = fileSystem;
    }

    public string Process(string url)
    {
        return fixPathToLocalMarkdownFile(url);
    }

    private string fixPathToLocalMarkdownFile(string url)
    {
        var isLocalFilePath = FilePathValidator.IsFilePath(url);
        var result = url;

        //System.Console.WriteLine($"\n\tUrl: {url}\n\tisLocaL: {isLocalFilePath}\n\tExtension: {FileSystem.Path.GetExtension(result)}");
        if (isLocalFilePath && (FileSystem.Path.GetExtension(result) == ".md"))
        {
            result = FileSystem.Path.ChangeExtension(result, ".html");
        }

        return result;
    }
}
