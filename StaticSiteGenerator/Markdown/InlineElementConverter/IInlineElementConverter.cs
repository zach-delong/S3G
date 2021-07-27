using StaticSiteGenerator.Markdown.InlineElement;
using Markdig.Syntax.Inlines;

namespace StaticSiteGenerator.Markdown.InlineElementConverter
{
    public interface IInlineElementConverter
    {
        IInlineElement Convert(IInline inline);
    }
}
