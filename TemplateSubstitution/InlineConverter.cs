using System.Collections.Generic;

using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using StaticSiteGenerator.TemplateSubstitution.InlineConverters;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [TransientService]
    public class InlineConverter: IConverter<MarkdownInline>, IConverter<IList<MarkdownInline>>
    {
        TextConverter TextConverter;

        public InlineConverter(TextConverter textConverter)
        {
            TextConverter = textConverter;
        }

        public void Convert(MarkdownInline inline)
        {
            switch(inline)
            {
                case TextRunInline i:

                    break;
            }
        }

        public void Convert(IList<MarkdownInline> inlines)
        {
            foreach(var inline in inlines)
            {
                Convert(inline);
            }
        }
    }
}
