using System;
using System.Text;
using System.Collections.Generic;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using StaticSiteGenerator.Markdown.InlineElement;

using StaticSiteGenerator.TemplateSubstitution.InlineConverters;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [TransientService]
    public class InlineConverter: IConverter<IInlineElement>, IConverter<IList<IInlineElement>>
    {
        TextConverter TextConverter;

        public InlineConverter(TextConverter textConverter)
        {
            TextConverter = textConverter;
        }

        public string Convert(IInlineElement inline)
        {
            switch(inline)
            {
                case Text i:
                    return TextConverter.Convert(i);
                default:
                    throw new ArgumentException(
                        message: $"inline {inline.GetType()} is not a recognized inline element",
                        paramName: nameof(inline));
            }
        }

        public string Convert(IList<IInlineElement> inlines)
        {
            var result = new StringBuilder();
            foreach(var inline in inlines)
            {
                try
                {
                    result.Append(Convert(inline));
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            return result.ToString();
        }
    }
}
