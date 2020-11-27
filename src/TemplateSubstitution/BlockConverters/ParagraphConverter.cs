using System;

using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [HtmlConverterFor(nameof(Paragraph))]
    public class ParagraphConverter: IConverter<IBlockElement>
    {
        private InlineConverter InlineConverter;
        private TemplateReader TemplateReader;

        public ParagraphConverter(InlineConverter inlineConverter, TemplateReader reader)
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
