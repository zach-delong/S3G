using System.Collections.Generic;
using FluentAssertions;
using Markdig.Syntax.Inlines;
using Moq.AutoMock;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.UnitTests.Doubles;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.Renderers;

public class LinkRendererTests
{

    MockTemplateTagCollectionFactory tagCollectionFactory => new MockTemplateTagCollectionFactory();
    HtmlStringWriterFactory htmlWriterFactory => new HtmlStringWriterFactory();

    [Theory]
    [MemberData(nameof(TestLinkData))]
    public void TestLinks(TestCaseDataObject testCaseData)
    {
        var mocker = new AutoMocker();

        var resultTag = new TemplateTag
        {
            Template = "<a href='{{url}}' property='thing'>{{display_text}}</a>"
        };

        mocker.MockTemplateTagCollection(resultTag);
        mocker.MockFileSystem(testCaseData.LocalFiles);
        mocker.MockLinkProcessor();

        var sut = mocker.CreateInstance<LinkRenderer>();

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, testCaseData.Input);

        Assert.Equal(testCaseData.Result, writer.ToString());

        writer
            .ToString()
            .Should().BeEquivalentTo(testCaseData.Result);

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
        var mocker = new AutoMocker();
        mocker.MockTemplateTagCollection(template);

        var sut = mocker.CreateInstance<LinkRenderer>();

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
        public readonly string Result;
        public readonly string[] LocalFiles;

        public TestCaseDataObject(LinkInline inline,
				  string expectedResult,
				  string[] localFiles)
        {
            Input = inline;
            Result = expectedResult;
            LocalFiles = localFiles;
        }

        public override string ToString()
        {
	    return $"Input Url: {Input?.Url}, Expected output: {Result}, File Cache: {string.Join(", ", LocalFiles)}";
        }
    }
}
