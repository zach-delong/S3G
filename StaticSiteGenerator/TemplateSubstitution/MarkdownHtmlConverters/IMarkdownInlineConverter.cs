using System.Collections.Generic;
using StaticSiteGenerator.Markdown.InlineElement;

namespace StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters
{
    public interface IMarkdownInlineConverter : IHtmlConverter<IInlineElement>, IHtmlConverter<IList<IInlineElement>>
    {
    }
}
