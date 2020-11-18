using StaticSiteGenerator.Markdown.InlineElement;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;

namespace StaticSiteGenerator.Markdown.InlineElementConverter
{
    public interface IInlineElementConverter
    {
        IInlineElement Convert(MarkdownInline inline);
    }
}
