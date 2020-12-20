using Xunit;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;

using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;

namespace Test.Markdown.ImnlineConverter
{
    public class TextConverterTest
    {
        [Theory]
        [InlineData("sample text")]
        [InlineData("")]
        public void TestTextConverter(string markdownInput)
        {
            var inline = new TextRunInline() { Text=markdownInput };
            var textConverter = new TextElementConverter();

            var result = textConverter.Convert(inline);

            Assert.Equal(markdownInput, result.Content);
        }
    }
}
