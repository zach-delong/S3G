using Xunit;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Markdig.Syntax.Inlines;
using Markdig.Helpers;

namespace StaticSiteGenerator.UnitTests.Markdown.InlineConverter;

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

        var result = textConverter.Execute(inline);

        Assert.Equal(markdownInput, result.Content.Replace("\n", ""));
    }
}
