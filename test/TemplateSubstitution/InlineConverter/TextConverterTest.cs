using NUnit.Framework;
using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.TemplateSubstitution.InlineConverters;

namespace Test.TemplateSubstitution.InlineConverter
{
    public class TextConverterTest
    {
        [TestCase("This is some sample text")]
        [TestCase("")]
        public void TestTextConverterCase(string input)
        {

          IHtmlConverter<IInlineElement> converter = new TextConverter();

          var TextElement = new Text {
              Content = input
          };

          var output = converter.Convert(TextElement);

          Assert.That(output, Is.EqualTo(input));
        }
    }
}
