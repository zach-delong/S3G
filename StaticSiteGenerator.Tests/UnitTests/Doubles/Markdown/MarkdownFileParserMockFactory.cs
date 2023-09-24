using System.Collections.Generic;
using AutoFixture;
using NSubstitute;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown.Parser;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;

public static class MarkdownFileParserMockFactory
{
    public static IMarkdownFileParser Get(IDictionary<string, IHtmlFile> input)
    {
        var mock = Substitute.For<IMarkdownFileParser>();

        mock
            .ReadFile(Arg.Any<string>())
            .Returns<IHtmlFile>((args) => {
                string fileName = args[0].ToString();
                return input[fileName];
            });

        return mock;
    }

    public static IMarkdownFileParser MockFileParser(this IFixture fixture, IDictionary<string, IHtmlFile> input)
    {
        var mock = Get(input);

        fixture.Inject<IMarkdownFileParser>(mock);

        return mock;
    }
}
