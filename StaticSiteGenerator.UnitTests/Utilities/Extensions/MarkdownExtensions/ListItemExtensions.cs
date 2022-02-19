using Markdig.Syntax;

namespace StaticSiteGenerator.UnitTests.Utilities.Extensions.MarkdownExtensions;

public static class ListItemExtensions
{
    public static void Add(this ListItemBlock block, string text)
    {
        var paragraph = new ParagraphBlock(null);

        paragraph.Add(text);

        block.Add(paragraph);
    }
}
