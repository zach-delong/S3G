using Markdig.Syntax;

namespace StaticSiteGenerator.Tests.UnitTests.Utilities.Extensions.MarkdownExtensions;

public static class ListBlockExtensions
{
    public static void Add(this ListBlock block, string text)
    {
        var item = new ListItemBlock(null);
        item.Add(text);

        block.Add(item);
    }
}
