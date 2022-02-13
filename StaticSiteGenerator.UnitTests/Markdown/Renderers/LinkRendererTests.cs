using System.Collections.Generic;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
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
    public void TestLinks(LinkInline inputBlock, string expectedOutput)
    {
        var resultTag = new TemplateTag
        {
            Template = "<a href='{{url}}' property='thing'>{{display_text}}</a>"
        };

        var tags = tagCollectionFactory.Get(resultTag);

        var sut = new LinkRenderer(tags.Object);

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, inputBlock);

        Assert.Equal(expectedOutput, writer.ToString());
    }

    public static IEnumerable<object[]> TestLinkData
    {
        get
        {
            yield return new object[]
            {
                new LinkInline {},
                "<a href='' property='thing'></a>"
            };

            yield return new object[]
            {
                new LinkInline { Url = "testing.html" },
                "<a href='testing.html' property='thing'></a>"
            };
        }
    }

    [Theory]
    [MemberData(nameof(TestImageData))]
    public void TestImages(LinkInline inputBlock, TemplateTag template, string expectedOutput)
    {

        var tags = tagCollectionFactory.Get(template);

        var sut = new LinkRenderer(tags.Object);

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
}
