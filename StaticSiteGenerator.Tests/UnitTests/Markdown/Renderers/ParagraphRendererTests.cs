using System.Collections.Generic;
using FluentAssertions;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.Markdown.Renderers;

public class ParagraphRendererTests
{
    MockTemplateTagCollectionFactory tagCollectionFactory => new MockTemplateTagCollectionFactory();
    HtmlStringWriterFactory htmlWriterFactory => new HtmlStringWriterFactory();

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

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[]
            {
                new ParagraphBlock {},
                "<p property='thing'></p>"
            };


            var inlineText = new Markdig.Helpers.StringSlice("Some test text", Markdig.Helpers.NewLine.LineFeed);

            var testTextContainer = new ContainerInline();
            testTextContainer.AppendChild(new LiteralInline(inlineText));

            var testTextParagraph = new ParagraphBlock { Inline = testTextContainer };

            yield return new object[]
            {
		testTextParagraph,
                "<p property='thing'>Some test text</p>"
            };

            var shouldBeHtmlEncoded = new Markdig.Helpers.StringSlice("Some test text <", Markdig.Helpers.NewLine.LineFeed);

            var shouldBeEncodedContainer = new ContainerInline();
            shouldBeEncodedContainer.AppendChild(new LiteralInline(inlineText));

            var encodedParagraph = new ParagraphBlock { Inline = shouldBeEncodedContainer };

            yield return new object[]
            {
		encodedParagraph,
                "<p property='thing'>Some test text &lt;</p>"
            };
        }
    }
}
