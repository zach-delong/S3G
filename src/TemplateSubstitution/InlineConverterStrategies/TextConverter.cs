using System;

using StaticSiteGenerator.Markdown.InlineElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.InlineConverterStrategies
{
    [HtmlConverterFor(nameof(Text))]
    public class TextConverter: IInlineConverterStrategy
    {
        public string Convert(IInlineElement inline)
        {
            var textInline = (Text) inline;
            return textInline.Content;
        }
    }
}
