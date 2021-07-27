using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateFilling;
using System;

namespace StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies
{
    [HtmlConverterFor(nameof(Paragraph))]
    public class ParagraphHtmlConverterStrategy : IBlockHtmlConverterStrategy
    {
        private readonly IMarkdownInlineConverter InlineConverter;
        private readonly ITemplateTagCollection TemplateTagCollection;
        private readonly ITemplateFiller TemplateFiller;

        public ParagraphHtmlConverterStrategy(IMarkdownInlineConverter inlineConverter,
                                          ITemplateTagCollection templateCollection,
                                          ITemplateFiller templateFiller)
        {
            InlineConverter = inlineConverter;
            TemplateTagCollection = templateCollection;
            TemplateFiller = templateFiller;
        }
        public string Convert(IBlockElement block)
        {
            var b = (Paragraph)block;
            var inlineText = String.Empty;

            if (b.Inlines != null)
            {
                inlineText = InlineConverter.Convert(b.Inlines);
            }

            var template = TemplateTagCollection.GetTagForType(TagType.Paragraph);

            return TemplateFiller.Fill(template, inlineText);
        }
    }
}
