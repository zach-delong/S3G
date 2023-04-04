using Markdig.Helpers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace StaticSiteGenerator.UnitTests.Utilities.Extensions.MarkdownExtensions;

public static class HeaderExtensions
{
    public static void Add(this HeadingBlock block, string text)
    {
        block.Inline ??= new ContainerInline();

        var inline = new LiteralInline { Content = new StringSlice(text) };

        block.Inline.AppendChild(inline);
    }
}
