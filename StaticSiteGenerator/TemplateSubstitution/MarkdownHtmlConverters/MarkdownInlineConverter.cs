using System;
using System.Text;
using System.Collections.Generic;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using StaticSiteGenerator.Markdown.InlineElement;

using StaticSiteGenerator.TemplateSubstitution.InlineConverterStrategies;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters
{
    [TransientService]
    public class MarkdownInlineConverter: IHtmlConverter<IInlineElement>, IHtmlConverter<IList<IInlineElement>>
    {
        StrategyCollection<IInlineConverterStrategy> InlineElementConverters;

        public MarkdownInlineConverter(StrategyCollection<IInlineConverterStrategy> inlineElementConverters)
        {
            InlineElementConverters = inlineElementConverters;
        }

        public virtual string Convert(IInlineElement inline)
        {
            var inlineConverter = InlineElementConverters.GetConverterForType(inline.GetType());

            return inlineConverter.Convert(inline);
        }

        public virtual string Convert(IList<IInlineElement> inlines)
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
