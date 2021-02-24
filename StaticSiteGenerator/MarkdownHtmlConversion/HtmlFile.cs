namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public class HtmlFile : IHtmlFile
    {
        public string HtmlContent { get; set; }
        public string Name { get; set; }
        public string FileExtension => "html";
    }
}
