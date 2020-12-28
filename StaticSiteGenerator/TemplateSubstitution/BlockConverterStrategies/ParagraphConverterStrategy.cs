using System;

using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies
{
    [HtmlConverterFor(nameof(Paragraph))]
    public class ParagraphConverterStrategy: IBlockHtmlConverterStrategy
    {
        private IMarkdownInlineConverter InlineConverter;
        private ITemplateTagCollection TemplateTagCollection;

        public ParagraphConverterStrategy(IMarkdownInlineConverter inlineConverter,
                                          ITemplateTagCollection templateCollection)
        {
            InlineConverter = inlineConverter;
            TemplateTagCollection = templateCollection;
        }
        public string Convert(IBlockElement block)
        {
            var b = (Paragraph) block;
            var inlineText = InlineConverter.Convert(b.Inlines);

            var template = TemplateTagCollection.GetTagForType(TagType.Paragraph);

            return template.ToHtml(inlineText);
        }
    }
}
