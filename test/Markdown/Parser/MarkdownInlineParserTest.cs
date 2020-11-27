using System;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;

namespace Test.Markdown.Parser
{
    public class MarkdownInlineParserTest
    {

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void TestConversionWithExistingConverter()
        {
            var converter = new TestConverter();
            var parser = new MarkdownInlineParser(new List<IInlineElementConverter> {
                    converter,
                });

            var inline = new TextRunInline();

            parser.Parse(inline);

            Assert.That(converter.ConverterCalled, Is.True);
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void TestConversionThrowsExceptionWithoutValidConverter()
        {
            var parser = new MarkdownInlineParser(new List<IInlineElementConverter>());

            var inline = new TextRunInline();

            Assert.Throws<Exception>(() => { parser.Parse(inline); });
        }

        [MarkdownConverterForAttribute(nameof(TextRunInline))]
        private class TestConverter: IInlineElementConverter
        {
            public bool ConverterCalled = false;
            public IInlineElement Convert(MarkdownInline inline)
            {
                ConverterCalled = true;
                return null;
            }
        }

    }
}