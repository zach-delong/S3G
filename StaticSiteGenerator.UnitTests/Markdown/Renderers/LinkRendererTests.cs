using System.Collections.Generic;
using Markdig.Syntax.Inlines;
using Moq.AutoMock;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
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
    public void TestLinks(TestCaseDataObject data)
    {
        var mocker = new AutoMocker();

        var resultTag = new TemplateTag
        {
            Template = "<a href='{{url}}' property='thing'>{{display_text}}</a>"
        };

        mocker.MockTemplateTagCollection(resultTag);
        mocker.MockFileExistenceChecker(data.LocalFiles);
        mocker.MockFileSystem(data.LocalFiles);

        var sut = mocker.CreateInstance<LinkRenderer>();

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, data.Input);

        Assert.Equal(data.Result, writer.ToString());
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

            // A link to a local markdown file should render as html

            yield return new TestCaseDataObject[] {
		new TestCaseDataObject (
		    new LinkInline { Url = "testing.md" },
		    "<a href='testing.html' property='thing'></a>",
		    new string[1] { "testing.md" }
		)
	    };

            // A link to a remote markdown file should render as md
            yield return new TestCaseDataObject[] {
		new TestCaseDataObject (
		    new LinkInline { Url = "http://stuff.com/testing.md" },
		    "<a href='http://stuff.com/testing.md' property='thing'></a>",
		    new string[0]
		)
	    };

            // A link to a local file that happens to contain ".md" but not as a file extension
            // remote markdown file should render as whatever it was
            yield return new TestCaseDataObject[] {
		new TestCaseDataObject (
		    new LinkInline { Url = "testing.md.html" },
		    "<a href='testing.md.html' property='thing'></a>",
		    new string[0]
		)
	    };

            // A link to a local markdown file should be transformed to a local html file 
            yield return new TestCaseDataObject[] {
		new TestCaseDataObject (
		    new LinkInline { Url = "testing.md" },
		    "<a href='testing.html' property='thing'></a>",
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

        Assert.Equal(expectedOutput, writer.ToString());
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
