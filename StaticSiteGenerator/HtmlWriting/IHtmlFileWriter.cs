namespace StaticSiteGenerator.HtmlWriting;

public interface IHtmlFileWriter
{
    void Write(string filePath, string htmlString);
}
