using System;

using StaticSiteGenerator.Markdown.InlineElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.InlineConverters
{
    [HtmlConverterFor(nameof(Text))]
    public class TextConverter: IHtmlConverter<IInlineElement>
    {
        public string Convert(IInlineElement inline)
        {
            var textInline = (Text) inline;
            return textInline.Content;
        }
    }
}
