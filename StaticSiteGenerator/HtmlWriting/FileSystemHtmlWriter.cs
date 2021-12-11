using StaticSiteGenerator.Files.FileWriting;

namespace StaticSiteGenerator.HtmlWriting;

public class FileSystemHtmlWriter : IHtmlFileWriter
{
    public FileSystemHtmlWriter(IFileWriter writer)
    {
        Writer = writer;
    }

    private const string HtmlFileExtension = ".html";
    private readonly IFileWriter Writer;

    public void Write(string filePath, string htmlString)
    {
        Writer.WriteFile(filePath, htmlString);
    }
}
