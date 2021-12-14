namespace StaticSiteGenerator.Markdown.InlineElement;

public class LinkElement : IInlineElement
{
    public string Link;
    public string Text;
    public string Content => throw new System.NotImplementedException();
}
