using System.Collections.Generic;
using Markdig.Syntax;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.Renderers;

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

        Assert.Equal(expectedOutput, writer.ToString());
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
        }
    }
}
