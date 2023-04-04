using System.Collections.Generic;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;
using StaticSiteGenerator.UnitTests.Utilities.Extensions.MarkdownExtensions;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.Renderers;

public class EmphasisRendererTests
{
    static MockTemplateTagCollectionFactory tagCollectionFactory => new MockTemplateTagCollectionFactory();
    HtmlStringWriterFactory htmlWriterFactory => new HtmlStringWriterFactory();

    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(EmphasisInline input, ITemplateTagCollection tags,string expectedOutput)
    {
        var sut = new EmphasisRenderer(tags);

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, input);

        Assert.Equal(expectedOutput, writer.ToString());
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            var boldWithText = new EmphasisInline
            {
                DelimiterCount = 2,
            };
            boldWithText.Add("Bold text");

            var italicsWithText = new EmphasisInline
            {
                DelimiterCount = 1,
            };
            italicsWithText.Add("Italics text");

            ITemplateTagCollection tagCollection = tagCollectionFactory
                .Get(new TemplateTag[]
                {
                    new TemplateTag { Template = "<span class='bold'>{{}}</span>", Type = TagType.Bold },
                    new TemplateTag { Template = "<span class='italic'>{{}}</span>", Type = TagType.Italic }
                })
                .Object;

            yield return new object[]
            {
                new EmphasisInline { DelimiterCount = 2 },
                tagCollection,
                "<span class='bold'></span>"
            };

            yield return new object[]
            {
                boldWithText,
                tagCollection,
                "<span class='bold'>Bold text</span>"
            };

            yield return new object[]
            {
                new EmphasisInline { DelimiterCount = 1 },
                tagCollection,
                "<span class='italic'></span>"
            };

            yield return new object[]
            {
                italicsWithText,
                tagCollection,
                "<span class='italic'>Italics text</span>"
            };
        }
    }
}
