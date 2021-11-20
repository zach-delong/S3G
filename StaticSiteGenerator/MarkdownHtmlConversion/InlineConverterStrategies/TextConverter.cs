using StaticSiteGenerator.Markdown.InlineElement;

namespace StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies
{
    [HtmlConverterFor(nameof(Text))]
    public class TextConverter : IInlineConverterStrategy
    {
        public string Execute(IInlineElement inline)
        {
            var textInline = (Text)inline;
            return textInline.Content.Trim();
        }
    }
}
