using System;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using Xunit;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace Test.Markdown.Parser
{
    public class MarkdownBlockParserTest
    {

        [Fact]
        public void TestConversionWithExistingConverter()
        {
            var converter = new TestConverter();
            var parser = new MarkdownBlockParser(new List<IBlockElementConverter> {
                    converter,
                });

            var header = new HeaderBlock();

            parser.Parse(header);

            Assert.True(converter.ConverterCalled);
        }

        [Fact]
        public void TestConversionThrowsExceptionWithoutValidConverter()
        {
            var parser = new MarkdownBlockParser(new List<IBlockElementConverter>());

            var block = new HeaderBlock();

            Assert.Throws<Exception>(() => { parser.Parse(block); });
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
