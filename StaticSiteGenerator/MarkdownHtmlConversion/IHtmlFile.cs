namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public interface IHtmlFile
    {
        public string HtmlContent { get; set; }
        public string Name { get; set; }
    }
}
