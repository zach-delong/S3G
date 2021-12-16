using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.InlineElement;

namespace StaticSiteGenerator.Markdown.InlineElementConverter;


[MarkdownConverterForAttribute(nameof(LinkInline))]
public class LinkElementConverter : IInlineElementConverter
{
    public delegate void BeforeLinkConversion(LinkInline input);
    public delegate void AfterLinkConversion(LinkInline input, LinkElement output);

    private BeforeLinkConversion PreLinkConversion;
    private AfterLinkConversion PostLinkConversion;

    public LinkElementConverter(
        BeforeLinkConversion preLinkConversion = null,
        AfterLinkConversion postLinkConversion = null)
    {
        PreLinkConversion = preLinkConversion;
        PostLinkConversion = postLinkConversion;
    }

    public IInlineElement Execute(IInline inline)
    {
        var element = (LinkInline)inline;

        if (element.IsImage)
        {
            return ConvertImage(element);
        }
        else
        {
            return ConvertLink(element);
        }
    }

    public IInlineElement ConvertLink(LinkInline inline)
    {
        PreLinkConversion?.Invoke(inline);

        var result = new LinkElement
        {
            Text = inline.Title,
            Link = inline.Url
        };

        PostLinkConversion?.Invoke(inline, result);

        return result;

    }

    public IInlineElement ConvertImage(LinkInline inline)
    {
        throw new System.NotImplementedException();
    }
}
