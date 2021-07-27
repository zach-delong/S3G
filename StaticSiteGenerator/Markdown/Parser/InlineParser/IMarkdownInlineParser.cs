using System.Collections.Generic;
using StaticSiteGenerator.Markdown.InlineElement;
using Markdig.Syntax.Inlines;

namespace StaticSiteGenerator.Markdown.Parser.InlineParser
{
    public interface IMarkdownInlineParser
    {
        IList<IInlineElement> Parse(ContainerInline inlines);
    }
}
