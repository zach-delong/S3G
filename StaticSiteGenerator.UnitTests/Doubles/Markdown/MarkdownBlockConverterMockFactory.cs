using Moq;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.UnitTests.Doubles.Markdown;

public class MarkdownBlockConverterMockFactory
{
    public Mock<IStrategyExecutor<string, IBlockElement>> Get()
    {
        var mock = new Mock<IStrategyExecutor<string, IBlockElement>>();

        return mock;
    }
}
