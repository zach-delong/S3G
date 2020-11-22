using System;
using NUnit.Framework;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.Markdown.BlockElementConverter;
using Test.Markdown.Doubles;

namespace Test.Markdown.BlockConverter
{
    public class ParagraphConverterTest
    {
        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void ParagraphConverterCallsInlineConverterTest()
        {
            var testInlineParser = new TestInlineParser();
            ParagraphConverter converter = new ParagraphConverter(testInlineParser);

            var paragraph = new ParagraphBlock();

            converter.Convert(paragraph);

            Assert.That(testInlineParser.ParseCalled, Is.True);

        }
    }
}
