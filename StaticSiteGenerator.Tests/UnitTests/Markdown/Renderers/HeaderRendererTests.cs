using System.Collections.Generic;
using FluentAssertions;
using Markdig.Syntax;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;
using StaticSiteGenerator.UnitTests.Utilities.Extensions.MarkdownExtensions;
using StaticSiteGenerator.Utilities;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.Renderers;

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
    public static MockTemplateTagCollectionFactory tagCollectionFactory => new MockTemplateTagCollectionFactory();

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
                tagCollectionFactory.Get(new TemplateTag { Template = "<h1 property='thing'>{{}}</h1>" }).Object,
                "<h1 property='thing'></h1>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 2 } ,
                tagCollectionFactory.Get(new TemplateTag { Template = "<h2 property='thing'>{{}}</h2>" }).Object,
                "<h2 property='thing'></h2>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 3 } ,
                tagCollectionFactory.Get(new TemplateTag { Template = "<h3 property='thing'>{{}}</h3>" }).Object,
                "<h3 property='thing'></h3>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 4 } ,
                tagCollectionFactory.Get(new TemplateTag { Template = "<h4 property='thing'>{{}}</h4>" }).Object,
                "<h4 property='thing'></h4>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 5 } ,
                tagCollectionFactory.Get(new TemplateTag { Template = "<h5 property='thing'>{{}}</h5>" }).Object,
                "<h5 property='thing'></h5>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 6 } ,
                tagCollectionFactory.Get(new TemplateTag { Template = "<h6 property='thing'>{{}}</h6>" }).Object,
                "<h6 property='thing'></h6>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 7 } ,
                tagCollectionFactory.Get(new TemplateTag { Template = "<h6 property='thing'>{{}}</h6>" }).Object,
                "<h6 property='thing'></h6>"
            };

            yield return new object[]
            {
                new HeadingBlock(null) { Level = 100 } ,
                tagCollectionFactory.Get(new TemplateTag { Template = "<h6 property='thing'>{{}}</h6>" }).Object,
                "<h6 property='thing'></h6>"
            };

            var headingWithText = new HeadingBlock(null);
            headingWithText.Level = 3;
            headingWithText.Add("This is a heading");

            yield return new object[]
            {
                headingWithText,
                tagCollectionFactory.Get(new TemplateTag { Template = "<h3 class='stuff'>{{}}</h3>"}).Object,
                "<h3 class='stuff'>This is a heading</h3>"
            };
        }
    }
}
