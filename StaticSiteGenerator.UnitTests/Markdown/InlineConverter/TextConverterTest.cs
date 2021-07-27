using Xunit;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;

using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Markdig.Syntax.Inlines;
using Markdig.Helpers;

namespace Test.Markdown.ImnlineConverter
{
    public class TextConverterTest
    {
        [Theory]
        [InlineData("sample text")]
        [InlineData("")]
        public void TestTextConverter(string markdownInput)
        {
            var inline = new LiteralInline
            {
                Content = new StringSlice(markdownInput),
            };
            var textConverter = new TextElementConverter();

            var result = textConverter.Convert(inline);

            Assert.Equal(markdownInput, result.Content.Replace("\n", ""));
        }
    }
}
