using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters
{
    public interface IMarkdownBlockConverter : IHtmlConverter<IList<IBlockElement>>, IHtmlConverter<IBlockElement>
    {
    }
}
