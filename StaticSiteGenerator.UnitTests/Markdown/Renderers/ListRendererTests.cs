using System.Collections.Generic;
using Markdig.Helpers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;
using StaticSiteGenerator.UnitTests.Utilities.Extensions.MarkdownExtensions;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.Renderers;

public class ListRendererTests
{
    MockTemplateTagCollectionFactory tagCollectionFactory => new MockTemplateTagCollectionFactory();
    HtmlStringWriterFactory htmlWriterFactory => new HtmlStringWriterFactory();

    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(ListBlock inputBlock, IEnumerable<TemplateTag> template, string expectedOutput)
    {
        var tags = tagCollectionFactory.Get(template);

        var sut = new ListRenderer(tags.Object);

        var (renderer, writer) = htmlWriterFactory.Get();

        sut.Write(renderer, inputBlock);

        Assert.Equal(expectedOutput, writer.ToString());
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
            orderedListWithOneItem.Add(getListItemWithText("Test Content"));

            var unorderedListWithOneItem = new ListBlock(null) { IsOrdered = false };
            unorderedListWithOneItem.Add(getListItemWithText("Test Content"));

            var unorderedListWithThreeItems = new ListBlock(null) { IsOrdered = false };
            unorderedListWithThreeItems.Add(getListItemWithText("Item 1"));
            unorderedListWithThreeItems.Add(getListItemWithText("Item 2"));
            unorderedListWithThreeItems.Add(getListItemWithText("Item 3"));

            var orderedListWithThreeItems = new ListBlock(null) { IsOrdered = true};
            orderedListWithThreeItems.Add(getListItemWithText("Item 1"));
            orderedListWithThreeItems.Add(getListItemWithText("Item 2"));
            orderedListWithThreeItems.Add(getListItemWithText("Item 3"));

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

    private static ListItemBlock getListItemWithText(string content)
    {
        var item = new ListItemBlock(null);
        var paragraph = new ParagraphBlock(null);

        paragraph.AddText(content);

        item.Add(paragraph);
        return item;
    }
}
