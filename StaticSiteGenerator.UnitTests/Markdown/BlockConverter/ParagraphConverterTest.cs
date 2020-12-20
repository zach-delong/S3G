using System;
using Xunit;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.Markdown.BlockElementConverter;
using StaticSiteGenerator.UnitTests.Markdown.Doubles;

namespace StaticSiteGenerator.UnitTests.Markdown.BlockConverter
{
    public class ParagraphConverterTest
    {
        [Fact]
        public void ParagraphConverterCallsInlineConverterTest()
        {
            var testInlineParser = new TestInlineParser();
            ParagraphConverter converter = new ParagraphConverter(testInlineParser);

            var paragraph = new ParagraphBlock();

            converter.Convert(paragraph);

            Assert.True(testInlineParser.ParseCalled);
        }
    }
}
