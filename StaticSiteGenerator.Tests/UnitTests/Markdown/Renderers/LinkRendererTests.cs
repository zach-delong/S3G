using System.Collections.Generic;
using FluentAssertions;
using Markdig.Syntax.Inlines;
using Moq.AutoMock;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Tests.UnitTests.Doubles;
using StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;
using StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.Markdown.Renderers;

public class LinkRendererTests
{

    HtmlStringWriterFactory htmlWriterFactory => new HtmlStringWriterFactory();

    [Theory]
    [MemberData(nameof(TestLinkData))]
    public void TestLinks(TestCaseDataObject testCaseData)
    {
        var resultTag = new TemplateTag
        {
            Template = "<a href='{{url}}' property='thing'>{{display_text}}</a>"
        };

        var templateTagCollection = MockTemplateTagCollectionFactory.Get(resultTag);
        var linkProcessor = LinkProcessorFactory.Get();

        var sut = new LinkRenderer(templateTagCollection, null, linkProcessor);

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, testCaseData.Input);

        writer
            .ToString()
            .Should().BeEquivalentTo(testCaseData.ExpectedResult);

    }

    public static IEnumerable<object[]> TestLinkData
    {
        get
        {
            // A blank inline should render a blank link
            yield return new TestCaseDataObject[] {
		new TestCaseDataObject
		(
		    new LinkInline {},
		    "<a href='' property='thing'></a>",
		    new string[0]
		)
	    };

            // An html link should stay the same
            yield return new TestCaseDataObject[] {
		new TestCaseDataObject(
		    new LinkInline { Url = "testing.html" },
		    "<a href='testing.html' property='thing'></a>",
		    new string[0]
		)
	    };

            yield return new TestCaseDataObject[] {
		new TestCaseDataObject (
		    new LinkInline { Url = "testing.md.html" },
		    "<a href='testing.md.html' property='thing'></a>",
		    new string[0]
		)
	    };
        }
    }

    [Theory]
    [MemberData(nameof(TestImageData))]
    public void TestImages(LinkInline inputBlock, TemplateTag template, string expectedOutput)
    {
        var tagCollection = MockTemplateTagCollectionFactory.Get(template);
        var sut = new LinkRenderer(tagCollection, null, null);

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, inputBlock);

        writer
            .ToString()
            .Should().BeEquivalentTo(expectedOutput);
    }

    public static IEnumerable<object[]> TestImageData
    {
        get
        {
            yield return new object[]
            {
                new LinkInline { IsImage = true },
                new TemplateTag
                {
                    Type = TagType.Image,
                    Template = "<a href='{{url}}'><img href='{{url}}' property='thing' /></a>"
                },
                "<a href=''><img href='' property='thing' /></a>"
            };

            yield return new object[]
            {
                new LinkInline { Url = "testing.png", IsImage = true },
                new TemplateTag
                {
                    Type = TagType.Image,
                    Template = "<a href='{{url}}'><img href='{{url}}' property='thing' /></a>"
                },
                "<a href='testing.png'><img href='testing.png' property='thing' /></a>"
            };

            yield return new object[]
            {
                new LinkInline { Url = "testing.png", IsImage = true },
                new TemplateTag
                {
                    Type = TagType.Image,
                    Template = "<img href='{{url}}' property='thing' />"
                },
                "<img href='testing.png' property='thing' />"
            };
        }
    }

    public class TestCaseDataObject
    {
        public readonly LinkInline Input;
        public readonly string ExpectedResult;
        public readonly string[] LocalFiles;

        public TestCaseDataObject(LinkInline inline,
				  string expectedResult,
				  string[] localFiles)
        {
            Input = inline;
            ExpectedResult = expectedResult;
            LocalFiles = localFiles;
        }

        public override string ToString()
        {
	    return $"Input Url: {Input?.Url}, Expected output: {ExpectedResult}, File Cache: {string.Join(", ", LocalFiles)}";
        }
    }
}
