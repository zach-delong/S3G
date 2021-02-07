using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown
{
    public interface IMarkdownFile
    {
        public IList<IBlockElement> Elements { get; set; }
        public string Name { get; set; }
    }
}
