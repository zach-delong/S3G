using Xunit;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;
using StaticSiteGenerator.UnitTests.Markdown.Doubles;
using Markdig.Syntax;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;

namespace Test.Markdown.BlockConverter;

public class HeaderConverterTest
{
    private StrategyPatternFactory strategyPatternFactory => new StrategyPatternFactory();

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public void ConversionTest(int headerLevel)
    {
        HeaderConverter converter = GetHeaderConverter();

        var blockInput = new HeadingBlock(null);

        blockInput.Level = headerLevel;
        blockInput.Inline = new ContainerInline();

        var result = (Header)converter.Execute(blockInput);

        Assert.Equal(result.Level, headerLevel);
    }

    private HeaderConverter GetHeaderConverter()
    {
        var parser = new TestInlineParser();
        return new HeaderConverter((IStrategyExecutor<IInlineElement, IInline>)parser);
    }
}
