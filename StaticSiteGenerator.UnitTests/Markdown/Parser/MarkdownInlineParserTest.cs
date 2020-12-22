using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Moq;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace Test.Markdown.Parser
{
    public class MarkdownInlineParserTest
    {
        private Mock<StrategyCollection<T>> GetMockStrategyCollection<T>(IDictionary<string, T> strategyMappings)
        {
            var mock = new Mock<StrategyCollection<T>>();

            mock.Setup(c => c.GetConverterForType(It.IsAny<Type>()))
                .Returns<Type>((p) => (T)strategyMappings[p.Name]);

            return mock;
        }
        [Fact]
        public void TestConversionWithExistingConverter()
        {
            var converter = new TestConverter();

            var parser = new MarkdownInlineParser(new List<IInlineElementConverter> {
                    converter,
                }, GetMockStrategyCollection<IInlineElementConverter>(new Dictionary<string, IInlineElementConverter>{
                { nameof(TextRunInline), converter }
            }).Object);

            var inline = new TextRunInline();

            parser.Parse(inline);

            Assert.True(converter.ConverterCalled);
        }

        [Fact]
        public void TestConversionThrowsExceptionWithoutValidConverter()
        {
            var parser = new MarkdownInlineParser(new List<IInlineElementConverter>(), new Mock<StrategyCollection<IInlineElementConverter>>().Object);

            var inline = new TextRunInline();

            // No exception should be thrown
            var result = parser.Parse(inline);

            Assert.Null(result);
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
