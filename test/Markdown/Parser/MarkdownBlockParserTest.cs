using System;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;

namespace Test.Markdown.Parser
{
    public class MarkdownBlockParserTest
    {

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void TestConversionWithExistingConverter()
        {
            var converter = new TestConverter();
            var parser = new MarkdownBlockParser(new List<IBlockElementConverter> {
                    converter,
                });

            var header = new HeaderBlock();

            parser.Parse(header);

            Assert.That(converter.ConverterCalled, Is.True);
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
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
