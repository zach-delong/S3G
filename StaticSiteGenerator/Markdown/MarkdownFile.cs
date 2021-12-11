using System;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown;

public class MarkdownFile : IMarkdownFile
{
    public IList<IBlockElement> Elements { get; set; }
    public string Name { get; set; }
    public string FileExtension => ".md";
    public DateTime PublishedDate { get; set; }
}
