using System.Collections.Generic;
using Xunit;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Moq;
using StaticSiteGenerator.Utilities.StrategyPattern;
using System.Linq;
using Microsoft.Extensions.Logging;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.UnitTests.Doubles;

namespace Test.Markdown.Parser
{
    public class MarkdownInlineParserTest
    {
        private StrategyCollectionMockFactory StrategyCollectionFactory => new StrategyCollectionMockFactory();
        private LoggerMockFactory loggerMockFactory => new LoggerMockFactory();

        [Fact]
        public void TestConversionWithExistingConverter()
        {
            var converter = new TestConverter();

            var strategyCollection = StrategyCollectionFactory.Get<IInlineElementConverter>(new Dictionary<string, IInlineElementConverter> {
                { nameof(LiteralInline), converter }
            }).Object;

            var parser = new MarkdownInlineParser(
                strategyCollection,
                loggerMockFactory.Get<MarkdownInlineParser>().Object
            );

            var inline = new LiteralInline();

            parser.Parse(inline);

            Assert.True(converter.ConverterCalled);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void TestConversionOfSetWithExistingConverter(int countOfElements)
        {
            var converter = new TestConverter();

            var parser = new MarkdownInlineParser(
                StrategyCollectionFactory.Get<IInlineElementConverter>(new Dictionary<string, IInlineElementConverter> { { nameof(LiteralInline), converter } }).Object,
                new Mock<ILogger<MarkdownInlineParser>>().Object);

            var input = new ContainerInline();


            foreach(var _ in Enumerable.Range(1, countOfElements))
            {
                input.AppendChild(new LiteralInline());
            }

            parser.Parse(input).ToList();

            Assert.Equal((countOfElements > 0) ? true : false, converter.ConverterCalled);
        }

        [Fact]
        public void TestConversionThrowsExceptionWithoutValidConverter()
        {
            var parser = new MarkdownInlineParser(
                new Mock<StrategyCollection<IInlineElementConverter>>(new List<IInlineElementConverter>()).Object,
                new Mock<ILogger<MarkdownInlineParser>>().Object);

            var inline = new LiteralInline();

            // No exception should be thrown
            var result = parser.Parse(inline);

            Assert.Null(result);
        }

        [MarkdownConverterForAttribute(nameof(LiteralInline))]
        private class TestConverter: IInlineElementConverter
        {
            public bool ConverterCalled = false;
            public IInlineElement Convert(IInline inline)
            {
                ConverterCalled = true;
                return null;
            }
        }

    }
}
