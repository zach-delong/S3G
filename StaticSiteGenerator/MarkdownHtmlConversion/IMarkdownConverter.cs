using System.Collections.Generic;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public interface IMarkdownConverter
    {
        IHtmlFile Convert(IMarkdownFile markdownFile);
    }
}
