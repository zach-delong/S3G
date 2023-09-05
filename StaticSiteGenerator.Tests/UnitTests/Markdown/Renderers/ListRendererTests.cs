using System.Collections.Generic;
using FluentAssertions;
using Markdig.Syntax;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;
using StaticSiteGenerator.Tests.UnitTests.Utilities.Extensions.MarkdownExtensions;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.Markdown.Renderers;

public class ListRendererTests
{
    HtmlStringWriterFactory htmlWriterFactory => new HtmlStringWriterFactory();

    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(ListBlock inputBlock, IEnumerable<TemplateTag> template, string expectedOutput)
    {
        var tags = MockTemplateTagCollectionFactory.Get(template);

        var sut = new ListRenderer(tags);

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, inputBlock);

        Assert.Equal(expectedOutput, writer.ToString());

        writer
            .ToString()
            .Should().BeEquivalentTo(expectedOutput);
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            var templates = new TemplateTag[]
            {
                new TemplateTag
                {
                    Type = TagType.UnorderedList,
                    Template = "<ul>{{}}</ul>",
                },
                new TemplateTag
                {
                    Type = TagType.OrderedList,
                    Template = "<ol>{{}}</ol>",
                },
                new TemplateTag
                {
                    Type = TagType.ListItem,
                    Template = "<li>{{}}</li>",
                }
            };

            var orderedListWithOneItem = new ListBlock(null) { IsOrdered = true };
            orderedListWithOneItem.Add("Test Content");

            var unorderedListWithOneItem = new ListBlock(null) { IsOrdered = false };
            unorderedListWithOneItem.Add("Test Content");

            var unorderedListWithThreeItems = new ListBlock(null) { IsOrdered = false };
            unorderedListWithThreeItems.Add("Item 1");
            unorderedListWithThreeItems.Add("Item 2");
            unorderedListWithThreeItems.Add("Item 3");

            var orderedListWithThreeItems = new ListBlock(null) { IsOrdered = true};
            orderedListWithThreeItems.Add("Item 1");
            orderedListWithThreeItems.Add("Item 2");
            orderedListWithThreeItems.Add("Item 3");

            yield return new object[]
            {
                new ListBlock (null)
                {
                    IsOrdered = false
                },
                templates,
                "<ul></ul>" // TODO
            };

            yield return new object[]
            {
                new ListBlock (null)
                {
                    IsOrdered = true
                },
                templates,
                "<ol></ol>" // TODO
            };

            yield return new object[]
            {
                orderedListWithOneItem,
                templates,
                "<ol><li><p>Test Content</p>\n</li></ol>"
            };

            yield return new object[]
            {
                orderedListWithOneItem,
                templates,
                "<ol><li><p>Test Content</p>\n</li></ol>"
            };

            yield return new object[]
            {
                unorderedListWithOneItem,
                templates,
                "<ul><li><p>Test Content</p>\n</li></ul>"
            };

            yield return new object[]
            {
                unorderedListWithThreeItems,
                templates,
                "<ul><li><p>Item 1</p>\n</li><li><p>Item 2</p>\n</li><li><p>Item 3</p>\n</li></ul>" 
            };

            yield return new object[]
            {
                orderedListWithThreeItems,
                templates,
                "<ol><li><p>Item 1</p>\n</li><li><p>Item 2</p>\n</li><li><p>Item 3</p>\n</li></ol>"
            };
        }
    }
}
