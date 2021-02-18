using System.Collections.Generic;
using Xunit;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Moq;
using StaticSiteGenerator.Utilities.StrategyPattern;
using System.Linq;

namespace Test.Markdown.Parser
{
    public class MarkdownInlineParserTest
    {
        private StrategyCollectionMockFactory StrategyCollectionFactory => new StrategyCollectionMockFactory();

        [Fact]
        public void TestConversionWithExistingConverter()
        {
            var converter = new TestConverter();

            var parser = new MarkdownInlineParser(
                StrategyCollectionFactory.Get<IInlineElementConverter>(new Dictionary<string, IInlineElementConverter> {
                { nameof(TextRunInline), converter }
            }).Object);

            var inline = new TextRunInline();

            parser.Parse(inline);

            Assert.True(converter.ConverterCalled);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void TestConversionOfSetWithExistingConverter(int countOfElements)
        {
            var converter = new TestConverter();

            var parser = new MarkdownInlineParser(
                StrategyCollectionFactory.Get<IInlineElementConverter>(new Dictionary<string, IInlineElementConverter> {
                { nameof(TextRunInline), converter }
            }).Object);

            var input = new List<MarkdownInline>();


            foreach(var _ in Enumerable.Range(1, countOfElements))
            {
                input.Add(new TextRunInline());
            }

            parser.Parse(input).ToList();

            Assert.Equal((countOfElements > 0) ? true : false, converter.ConverterCalled);
        }

        [Fact]
        public void TestConversionThrowsExceptionWithoutValidConverter()
        {
            var parser = new MarkdownInlineParser(new Mock<StrategyCollection<IInlineElementConverter>>(new List<IInlineElementConverter>()).Object);

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
