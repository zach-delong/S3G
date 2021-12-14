using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.InlineConverter;

public class LinkConverterTests
{
    [Theory]
    [InlineData("foo", "foo/bar")]
    [InlineData("", "foo/bar")]
    [InlineData("foo", "")]
    [InlineData("", "")]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void TestConverter(string title, string url)
    {
        var inline = new LinkInline(url, title);

        var converter = new LinkElementConverter();

        var result = (LinkElement)converter.Execute(inline);

        Assert.Equal(inline.Title, result.Text);
        Assert.Equal(inline.Url, result.Link);
    }
}
