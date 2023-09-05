using System.Collections.Generic;
using FluentAssertions;
using Markdig.Syntax;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;
using StaticSiteGenerator.Tests.UnitTests.Utilities.Extensions.MarkdownExtensions;
using StaticSiteGenerator.Utilities;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.Markdown.Renderers;

public class BlockTestWriter
{
    public HtmlStringWriterFactory htmlWriterFactory => new HtmlStringWriterFactory();

    public void runTest<T>(CustomRendererBase<T> customRenderer, T inputBlock, string expectedOutput) where T : LeafBlock
    {
        var (renderer, writer) = htmlWriterFactory.Get();

        customRenderer.Write(renderer, inputBlock);

        writer
            .ToString()
            .Should().BeEquivalentTo(expectedOutput);
    }
}

public class HeaderRendererTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(HeadingBlock blockToWrite, ITemplateTagCollection tags, string expectedResult)
    {
        BlockTestWriter testHelper = new BlockTestWriter();
        var sut = new HeaderRenderer(tags, new HeaderLevelHelper());

        testHelper.runTest<HeadingBlock>(sut, blockToWrite, expectedResult);

        testHelper.Invoking(h => h.runTest<HeadingBlock>(sut, blockToWrite, expectedResult))
            .Should().NotThrow();
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[]
            {
                new HeadingBlock(null),
                MockTemplateTagCollectionFactory.Get(new TemplateTag { Template = "<h1 property='thing'>{{}}</h1>" }),
                "<h1 property='thing'></h1>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 2 } ,
                MockTemplateTagCollectionFactory.Get(new TemplateTag { Template = "<h2 property='thing'>{{}}</h2>" }),
                "<h2 property='thing'></h2>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 3 } ,
                MockTemplateTagCollectionFactory.Get(new TemplateTag { Template = "<h3 property='thing'>{{}}</h3>" }),
                "<h3 property='thing'></h3>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 4 } ,
                MockTemplateTagCollectionFactory.Get(new TemplateTag { Template = "<h4 property='thing'>{{}}</h4>" }),
                "<h4 property='thing'></h4>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 5 } ,
                MockTemplateTagCollectionFactory.Get(new TemplateTag { Template = "<h5 property='thing'>{{}}</h5>" }),
                "<h5 property='thing'></h5>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 6 } ,
                MockTemplateTagCollectionFactory.Get(new TemplateTag { Template = "<h6 property='thing'>{{}}</h6>" }),
                "<h6 property='thing'></h6>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 7 } ,
                MockTemplateTagCollectionFactory.Get(new TemplateTag { Template = "<h6 property='thing'>{{}}</h6>" }),
                "<h6 property='thing'></h6>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 100 } ,
                MockTemplateTagCollectionFactory.Get(new TemplateTag { Template = "<h6 property='thing'>{{}}</h6>" }),
                "<h6 property='thing'></h6>"
            };

            var headingWithText = new HeadingBlock(null);
            headingWithText.Level = 3;
            headingWithText.Add("This is a heading");

            yield return new object[]
            {
                headingWithText,
                MockTemplateTagCollectionFactory.Get(new TemplateTag { Template = "<h3 class='stuff'>{{}}</h3>"}),
                "<h3 class='stuff'>This is a heading</h3>"
            };
        }
    }
}
