using System;

using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Markdown.BlockElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies
{
    [HtmlConverterFor(nameof(Header))]
    public class HeaderConverterStrategy: IBlockHtmlConverterStrategy
    {
        private MarkdownInlineConverter InlineConverter;
        private TemplateReader TemplateReader;

        public HeaderConverterStrategy(MarkdownInlineConverter inlineConverter, TemplateReader reader)
        {
            InlineConverter = inlineConverter;
            TemplateReader = reader;
        }

        public string Convert(IBlockElement block)
        {
            var template = TemplateReader.GetTemplateTagForType(TagType.Header1);
            Console.WriteLine(template);
            var inlineText = InlineConverter.Convert(((Header)block).Inlines);
            Console.WriteLine(inlineText);
            return template.ToHtml(inlineText);
        }
    }
}
