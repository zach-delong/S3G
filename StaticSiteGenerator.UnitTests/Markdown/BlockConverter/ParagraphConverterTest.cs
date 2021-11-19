using System;
using Xunit;

using StaticSiteGenerator.Markdown.BlockElementConverter;
using StaticSiteGenerator.UnitTests.Markdown.Doubles;
using StaticSiteGenerator.UnitTests.Doubles;
using Markdig.Syntax;

namespace StaticSiteGenerator.UnitTests.Markdown.BlockConverter
{
    public class ParagraphConverterTest
    {
        private LoggerMockFactory loggerMockFactory => new LoggerMockFactory();

        [Fact]
        public void ParagraphConverterCallsInlineConverterTest()
        {
            var testInlineParser = new TestInlineParser();
            var logger = loggerMockFactory.Get<ParagraphConverter>().Object;

            ParagraphConverter converter = new ParagraphConverter(testInlineParser, logger);

            var paragraph = new ParagraphBlock();

            converter.Execute(paragraph);

            Assert.True(testInlineParser.ParseCalled);
        }
    }
}
