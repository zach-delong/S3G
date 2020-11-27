using System;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [TransientService]
    public class HeaderConverter : IConverter<Header>
    {
        private InlineConverter InlineConverter;
        private TemplateReader TemplateReader;

        public HeaderConverter(InlineConverter inlineConverter, TemplateReader reader)
        {
            InlineConverter = inlineConverter;
            TemplateReader = reader;
        }

        public string Convert(Header block)
        {
            var template = TemplateReader.GetTemplateTagForType(TagType.Header1);
            var inlineText = InlineConverter.Convert(block.Inlines);
            return template.ToHtml(inlineText);
        }
    }
}
