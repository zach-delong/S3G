using System;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using Xunit;
using StaticSiteGenerator.UnitTests.Doubles;
using Markdig.Syntax;
using StaticSiteGenerator.Utilities.StrategyPattern.Exceptions;

namespace Test.Markdown.Parser
{
    public class MarkdownBlockParserTest
    {
        StrategyCollectionMockFactory mockFactory => new StrategyCollectionMockFactory();
        LoggerMockFactory loggerMockFactory => new LoggerMockFactory();

        [Fact]
        public void TestConversionWithExistingConverter()
        {
            var converter = new TestConverter();
            var mock = mockFactory.Get(new Dictionary<string, IBlockElementConverter>
            {
                { nameof(HeadingBlock), converter }
            });
            var logger = loggerMockFactory.Get<MarkdownBlockParser>();

            var parser = new MarkdownBlockParser(
                mock.Object,
                logger.Object);

            var header = new HeadingBlock(null);

            parser.Parse(header);

            Assert.True(converter.ConverterCalled);
        }

        [Fact]
        public void TestConversionThrowsExceptionWithoutValidConverter()
        {
            var mock = mockFactory.Get(new Dictionary<string, IBlockElementConverter>());
            var loggerMock = loggerMockFactory.Get<MarkdownBlockParser>();
            var parser = new MarkdownBlockParser(mock.Object, loggerMock.Object);

            var block = new HeadingBlock(null);

            Assert.Throws<StrategyNotFoundException>(() => { parser.Parse(block); });
        }

        [MarkdownConverterForAttribute(nameof(HeadingBlock))]
        private class TestConverter: IBlockElementConverter
        {
            public bool ConverterCalled = false;
            public IBlockElement Convert(IBlock block)
            {
                ConverterCalled = true;
                return null;
            }
        }
    }
}
