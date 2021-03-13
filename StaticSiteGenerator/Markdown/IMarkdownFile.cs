using System;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown
{
    public interface IMarkdownFile
    {
        public IList<IBlockElement> Elements { get; set; }
        public string Name { get; set; }
        public string FileExtension { get; }
        public DateTime PublishedDate { get; set; }
    }
}
