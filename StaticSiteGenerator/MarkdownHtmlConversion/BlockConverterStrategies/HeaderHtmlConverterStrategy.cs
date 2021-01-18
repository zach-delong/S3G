using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters;

namespace StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies
{
    [HtmlConverterFor(nameof(Header))]
    public class HeaderHtmlConverterStrategy : IBlockHtmlConverterStrategy
    {
        private readonly IMarkdownInlineConverter InlineConverter;
        private readonly ITemplateTagCollection TemplateTagCollection;

        public readonly ITemplateFiller TemplateFiller;

        public HeaderHtmlConverterStrategy(IMarkdownInlineConverter inlineConverter,
                                       ITemplateTagCollection templateCollection,
                                       ITemplateFiller templateFiller)
        {
            InlineConverter = inlineConverter;
            TemplateTagCollection = templateCollection;
            TemplateFiller = templateFiller;
        }

        public string Convert(IBlockElement block)
        {
            var template = TemplateTagCollection.GetTagForType(TagType.Header1);
            var inlineText = InlineConverter.Convert(((Header)block).Inlines);

            return TemplateFiller.Fill(template, inlineText);
        }
    }
}
