using System;

using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [HtmlConverterFor(nameof(Paragraph))]
    public class ParagraphConverter: IHtmlConverter<IBlockElement>
    {
        private MarkdownInlineConverter InlineConverter;
        private TemplateReader TemplateReader;

        public ParagraphConverter(MarkdownInlineConverter inlineConverter, TemplateReader reader)
        {
            InlineConverter = inlineConverter;
            TemplateReader = reader;
        }
        public string Convert(IBlockElement block)
        {
            var b = (Paragraph) block;
            var inlineText = InlineConverter.Convert(b.Inlines);

            var template = TemplateReader.GetTemplateTagForType(TagType.Paragraph);

            return template.ToHtml(inlineText);
        }
    }
}
