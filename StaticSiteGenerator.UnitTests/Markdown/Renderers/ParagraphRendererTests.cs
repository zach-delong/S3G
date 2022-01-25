using System.Collections.Generic;
using System.IO;
using Markdig.Renderers;
using Markdig.Syntax;
using Moq;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.Renderers;

public class ParagraphRendererTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(ParagraphBlock inputBlock, string expectedOutput)
    {
        var resultTag = new TemplateTag
        {
            Template = "<p property='thing'>{{}}</p>"
        };

        var tags = new Mock<ITemplateTagCollection>();

        tags
            .Setup(c => c.GetTagForType(It.IsAny<TagType>()))
            .Returns(resultTag);

        var sut = new ParagraphRenderer(tags.Object);

        StringWriter writer = new StringWriter();
        var htmlRenderer = new HtmlRenderer(writer);

        sut.Write(htmlRenderer, inputBlock);

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
