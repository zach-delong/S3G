using Xunit;
using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.TemplateSubstitution.InlineConverterStrategies;

namespace StaticSiteGenerator.UnitTests.TemplateSubstitution.InlineConverter
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
