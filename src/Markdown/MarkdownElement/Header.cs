namespace StaticSiteGenerator.Markdown.MarkdownElement
{
    public class Header: IMarkdownElement
    {
        public byte Level { get; set; }
        public string Text { get; set; }

        public string Content => Text;
    }
}
