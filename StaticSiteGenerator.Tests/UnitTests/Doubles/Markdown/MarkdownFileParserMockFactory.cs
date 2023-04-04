using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown.Parser;

namespace StaticSiteGenerator.UnitTests.Doubles.Markdown;

public static class MarkdownFileParserMockFactory
{
    public static Mock<IMarkdownFileParser> Get(IDictionary<string, IHtmlFile> input)
    {
        var mock = new Mock<IMarkdownFileParser>();

        mock
            .Setup(m => m.ReadFile(It.IsAny<string>()))
            .Returns<string>(fileName => input[fileName]);

        return mock;
    }
}
