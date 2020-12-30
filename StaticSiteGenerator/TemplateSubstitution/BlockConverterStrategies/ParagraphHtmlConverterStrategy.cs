using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies
{
    [HtmlConverterFor(nameof(Paragraph))]
    public class ParagraphHtmlConverterStrategy: IBlockHtmlConverterStrategy
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
            var b = (Paragraph) block;
            var inlineText = InlineConverter.Convert(b.Inlines);
            var template = TemplateTagCollection.GetTagForType(TagType.Paragraph);

            return TemplateFiller.Fill(template, inlineText);
        }
    }
}
