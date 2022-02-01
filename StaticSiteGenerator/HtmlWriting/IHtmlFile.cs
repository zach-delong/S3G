namespace StaticSiteGenerator.HtmlWriting;

public interface IHtmlFile
{
    public string HtmlContent { get; set; }
    public string Name { get; set; }
    public string FileExtension { get; }
}
