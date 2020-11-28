using System;

using StaticSiteGenerator.Markdown.InlineElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.InlineConverters
{
    [TransientService]
    public class TextConverter: IHtmlConverter<Text>
    {
        public string Convert(Text inline)
        {
            return inline.Content;
        }
    }
}
