using StaticSiteGenerator.Markdown.InlineElement;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Markdown.Parser.InlineParser
{
    public interface IMarkdownInlineParser: IStrategyExcecutor<IInline, IInlineElement>
    {
    }
}
