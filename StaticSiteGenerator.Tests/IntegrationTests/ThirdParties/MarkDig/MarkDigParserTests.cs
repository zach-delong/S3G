using System.Collections.Generic;
using FluentAssertions;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests.ThirdParties.Markdown;

public class MarkDigParserTests
{
    [Fact]
    public void BasicConversion()
    {
	var exampleInput = "# Hello, world!";

        var document = Markdig.Markdown.ToHtml(exampleInput);

        document.Should().BeEquivalentTo("<h1>Hello, world!</h1>\n");
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(ParagraphBlock inputBlock, string expectedOutput)
    {
        var resultTag = new TemplateTag
        {
            Template = "<p property='thing'>{{}}</p>"
        };

        var tags = tagCollectionFactory.Get(resultTag);

        var sut = new ParagraphRenderer(tags.Object);

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, inputBlock);

        writer
            .ToString()
            .Should().BeEquivalentTo(expectedOutput);
    }

    MockTemplateTagCollectionFactory tagCollectionFactory => new MockTemplateTagCollectionFactory();
    HtmlStringWriterFactory htmlWriterFactory => new HtmlStringWriterFactory();

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[]
            {
                new ParagraphBlock {},
                "<p property='thing'></p>"
            };

            yield return new object[]
            {
		MarkdigObjectFactory.GetParagraphBlock("Some test text"),
                "<p property='thing'>Some test text</p>"
            };

            yield return new object[]
            {
		MarkdigObjectFactory.GetParagraphBlock("Some test text <"),
                "<p property='thing'>Some test text &lt;</p>"
            };
        }
    }
}


public static class MarkdigObjectFactory
{
    public static ParagraphBlock GetParagraphBlock(string contents)
    {
	var shouldBeHtmlEncoded = new Markdig.Helpers.StringSlice(contents, Markdig.Helpers.NewLine.LineFeed);

	var shouldBeEncodedContainer = new ContainerInline();
	shouldBeEncodedContainer.AppendChild(new LiteralInline(shouldBeHtmlEncoded));

	return new ParagraphBlock { Inline = shouldBeEncodedContainer };

    }
}
