namespace StaticSiteGenerator.HtmlWriting;

public class HtmlFile : IHtmlFile
{
    public string HtmlContent { get; set; }
    public string Name { get; set; }
    public string FileExtension => "html";

    public bool IsPublished { get; set; }
    public string Title { get; set; }
}
