using System.Collections.Generic;
using FluentAssertions;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.Markdown.Renderers;

public class ParagraphRendererTests
{
    HtmlStringWriterFactory htmlWriterFactory => new HtmlStringWriterFactory();

    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(ParagraphBlock inputBlock, string expectedOutput)
    {
        var sut = new ParagraphRenderer();

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, inputBlock);

        writer
            .ToString()
            .Should().BeEquivalentTo(expectedOutput);
    }


    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[]
            {
                new ParagraphBlock {},
                "<p></p>\n"
            };

            yield return new object[]
            {
		ParagraphHelper.Get("Some test text"),
                "<p>Some test text</p>\n"
            };

            yield return new object[]
            {
		ParagraphHelper.Get("Some test text <"),
                "<p>Some test text &lt;</p>\n"
            };

            yield return new object[]
            {
		ParagraphHelper.Get("Some test text ="),
                "<p>Some test text =</p>\n"
            };
        }
    }
}

public static class ParagraphHelper
{
    public static ParagraphBlock Get(string contents)
    {
	var shouldBeHtmlEncoded = new Markdig.Helpers.StringSlice(contents, Markdig.Helpers.NewLine.LineFeed);

	var shouldBeEncodedContainer = new ContainerInline();
	shouldBeEncodedContainer.AppendChild(new LiteralInline(shouldBeHtmlEncoded));

	return new ParagraphBlock { Inline = shouldBeEncodedContainer };

    }
}
