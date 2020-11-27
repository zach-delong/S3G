using System.Collections.Generic;

using Microsoft.Toolkit.Parsers.Markdown.Inlines;

using StaticSiteGenerator.Markdown.InlineElement;

namespace StaticSiteGenerator.Markdown.Parser.InlineParser
{
    public interface IMarkdownInlineParser
    {
        IList<IInlineElement> Parse(IList<MarkdownInline> inlines);
    }
}
