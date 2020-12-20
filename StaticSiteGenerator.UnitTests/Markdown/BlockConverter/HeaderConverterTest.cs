using Xunit;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using StaticSiteGenerator.UnitTests.Markdown.Doubles;

namespace Test.Markdown.BlockConverter
{
    public class HeaderConverterTest
    {
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

            var blockInput = new HeaderBlock {
                HeaderLevel = headerLevel
            };

            var result = (Header) converter.Convert(blockInput);

            Assert.Equal(result.Level, headerLevel);
        }

        private HeaderConverter GetHeaderConverter(){
            return new HeaderConverter(new TestInlineParser());
        }

    }
}
