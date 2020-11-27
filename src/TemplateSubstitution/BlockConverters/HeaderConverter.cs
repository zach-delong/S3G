using System;

using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Markdown.BlockElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [HtmlConverterFor(nameof(Header))]
    public class HeaderConverter : IConverter<IBlockElement>
    {
        private InlineConverter InlineConverter;
        private TemplateReader TemplateReader;

        public HeaderConverter(InlineConverter inlineConverter, TemplateReader reader)
        {
            InlineConverter = inlineConverter;
            TemplateReader = reader;
        }

        public string Convert(IBlockElement block)
        {
            var template = TemplateReader.GetTemplateTagForType(TagType.Header1);
            var inlineText = InlineConverter.Convert(((Header)block).Inlines);
            return template.ToHtml(inlineText);
        }
    }
}
