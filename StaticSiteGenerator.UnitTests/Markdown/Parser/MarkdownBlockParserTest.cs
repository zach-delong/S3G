using System;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using Xunit;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace Test.Markdown.Parser
{
    public class MarkdownBlockParserTest
    {
        StrategyCollectionMockFactory mockFactory => new StrategyCollectionMockFactory();

        [Fact]
        public void TestConversionWithExistingConverter()
        {
            var converter = new TestConverter();
            var mock = mockFactory.Get(new Dictionary<string, IBlockElementConverter>
            {
                { nameof(HeaderBlock), converter }
            });

            var parser = new MarkdownBlockParser(mock.Object);

            var header = new HeaderBlock();

            parser.Parse(header);

            Assert.True(converter.ConverterCalled);
        }

        [Fact]
        public void TestConversionThrowsExceptionWithoutValidConverter()
        {
            var mock = mockFactory.Get(new Dictionary<string, IBlockElementConverter>());
            var parser = new MarkdownBlockParser(mock.Object);

            var block = new HeaderBlock();

            Assert.Throws<StrategyNotFoundException>(() => { parser.Parse(block); });
        }

        [MarkdownConverterForAttribute(nameof(HeaderBlock))]
        private class TestConverter: IBlockElementConverter
        {
            public bool ConverterCalled = false;
            public IBlockElement Convert(MarkdownBlock block)
            {
                ConverterCalled = true;
                return null;
            }
        }
    }
}
