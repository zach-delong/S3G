using NUnit.Framework;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using Test.Markdown.Doubles;

namespace Test.Markdown.BlockConverter
{
    public class HeaderConverterTest
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [Parallelizable(ParallelScope.Self)]
        public void ConversionTest(int headerLevel)
        {
            HeaderConverter converter = GetHeaderConverter();

            var blockInput = new HeaderBlock {
                HeaderLevel = headerLevel
            };

            var result = (Header) converter.Convert(blockInput);

            Assert.That(result.Level, Is.EqualTo(headerLevel));
        }

        private HeaderConverter GetHeaderConverter(){
            return new HeaderConverter(new TestInlineParser());
        }

    }
}
