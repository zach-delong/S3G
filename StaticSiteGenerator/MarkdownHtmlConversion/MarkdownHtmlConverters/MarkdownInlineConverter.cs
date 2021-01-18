using System;
using System.Text;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Utilities.StrategyPattern;
using StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;

namespace StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters
{
    public class MarkdownInlineConverter : IMarkdownInlineConverter
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
            foreach (var inline in inlines)
            {
                try
                {
                    result.Append(Convert(inline));
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            return result.ToString();
        }
    }
}
