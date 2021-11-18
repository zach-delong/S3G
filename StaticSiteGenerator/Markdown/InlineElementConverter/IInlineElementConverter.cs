using StaticSiteGenerator.Markdown.InlineElement;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Markdown.InlineElementConverter
{
    public interface IInlineElementConverter: IStrategy<IInline, IInlineElement>
    {
        new IInlineElement Execute(IInline inline);
    }
}
