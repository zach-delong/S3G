using System.Collections.Generic;

using StaticSiteGenerator.Markdown.MarkdownElement;

namespace StaticSiteGenerator.Markdown
{
    public interface IMarkdownParser
    {
        public IList<IMarkdownElement> Parse(string markdownFile);
    }
}
