using Xunit;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using StaticSiteGenerator.UnitTests.Markdown.Doubles;
using Markdig.Syntax;

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

            var blockInput = new HeadingBlock(null);

            blockInput.Level = headerLevel;

            var result = (Header)converter.Convert(blockInput);

            Assert.Equal(result.Level, headerLevel);
        }

        private HeaderConverter GetHeaderConverter()
        {
            return new HeaderConverter(new TestInlineParser());
        }

    }
}
