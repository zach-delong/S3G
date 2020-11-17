
namespace StaticSiteGenerator.Markdown.MarkdownElement
{
    public class Paragraph: IMarkdownElement
    {
        public string Text { get; set; }

        public string Content => Text;
    }
}
