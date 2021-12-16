using System.Collections.Generic;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.MarkdownHtmlConversion.TagModelConverters;
using Xunit;

namespace StaticSiteGenerator.UnitTests.MarkdownHtmlConversion.TagModelConverters;

public class LinkInlineModelConverterTest
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(LinkElement input)
    {
        var sut = new LinkInlineModelConverter();
        var result = sut.Convert(input);

        Assert.Contains("url", result.Keys);
        Assert.Equal(input.Link, result["url"]);

        Assert.Contains("display_text", result.Keys);
        Assert.Equal(input.Text, result["display_text"]);
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[] { new LinkElement() };

            yield return new object[] { new LinkElement { Text = "Stuff" } };

            yield return new object[] { new LinkElement { Link = "Stuff" } };

            yield return new object[] { new LinkElement { Text = "Things", Link = "Stuff" } };
        }
    }
}
