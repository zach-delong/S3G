using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.InlineElement;

namespace StaticSiteGenerator.Markdown.InlineElementConverter;


[MarkdownConverterForAttribute(nameof(LinkInline))]
public class LinkElementConverter : IInlineElementConverter
{
    public IInlineElement Execute(IInline inline)
    {
        var element = (LinkInline)inline;

        if(element.IsImage)
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
        return new LinkElement
        {
            Text = inline.Title,
            Link = inline.Url
        };
    }

    public IInlineElement ConvertImage(LinkInline inline)
    {
        throw new System.NotImplementedException();
    }
}
