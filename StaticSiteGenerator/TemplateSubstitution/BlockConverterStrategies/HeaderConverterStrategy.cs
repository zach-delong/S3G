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
        private IMarkdownInlineConverter InlineConverter;
        private ITemplateTagCollection TemplateTagCollection;

        public HeaderConverterStrategy(IMarkdownInlineConverter inlineConverter,
                                       ITemplateTagCollection reader)
        {
            InlineConverter = inlineConverter;
            TemplateTagCollection = reader;
        }

        public string Convert(IBlockElement block)
        {
            var template = TemplateTagCollection.GetTagForType(TagType.Header1);
            Console.WriteLine(template);
            var inlineText = InlineConverter.Convert(((Header)block).Inlines);
            Console.WriteLine(inlineText);
            return template.ToHtml(inlineText);
        }
    }
}
