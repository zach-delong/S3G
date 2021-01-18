using Xunit;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;

namespace StaticSiteGenerator.UnitTests.MarkdownHtmlConversion.InlineConverter
{
    public class TextConverterTest
    {
        [Theory]
        [InlineData("This is some sample text")]
        [InlineData("")]
        public void TestTextConverterCase(string input)
        {

          IHtmlConverter<IInlineElement> converter = new TextConverter();

          var TextElement = new Text {
              Content = input
          };

          var output = converter.Convert(TextElement);

          Assert.Equal(output, input);
        }
    }
}
