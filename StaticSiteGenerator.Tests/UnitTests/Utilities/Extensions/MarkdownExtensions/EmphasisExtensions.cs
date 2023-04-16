using Markdig.Helpers;
using Markdig.Syntax.Inlines;

namespace StaticSiteGenerator.Tests.UnitTests.Utilities.Extensions.MarkdownExtensions;

public static class EmphasisExtensions
{
    public static void Add(this EmphasisInline input, string text)
    {
        var inline = new LiteralInline { Content = new StringSlice(text) };

        input.AppendChild(inline);
    }
}
