using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters
{
    public interface IMarkdownBlockConverter: IHtmlConverter<IList<IBlockElement>>, IHtmlConverter<IBlockElement>
    {
    }
}
