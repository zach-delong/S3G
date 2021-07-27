using System;
using System.Collections.Generic;
using Xunit;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Test.Markdown.Parser;
using StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;
using StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.UnitTests.Doubles;

namespace Test.MarkdownHtmlConversion
{
    public class MarkdownInlineConverterTest
    {
        private StrategyCollectionMockFactory mockFactory => new StrategyCollectionMockFactory();
        private LoggerMockFactory logMockFactory => new LoggerMockFactory();

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ConverterCallsCorrectStrategyWhenExists(int numInlines)
        {
            TestConverter testConverter = new TestConverter();
            Dictionary<string, IInlineConverterStrategy> strategyMappings = new Dictionary<string, IInlineConverterStrategy>
            {
                { nameof(Text), testConverter }
            };

            var mock = mockFactory.Get<IInlineConverterStrategy>(strategyMappings);
            var loggerMock = logMockFactory.Get<MarkdownInlineConverter>();

            var converter = new MarkdownInlineConverter(mock.Object, loggerMock.Object);

            var inline = new Text();

            var inlines = new List<IInlineElement>();

            for (var i = 0; i < numInlines; i++)
            {
                inlines.Add(inline);
            }

            converter.Convert(inlines);

            if(numInlines > 0)
            {
                Assert.True(testConverter.ConverterCalled);
            }
            else
            {
                Assert.False(testConverter.ConverterCalled);
            }
        }

        [Fact]
        public void ConverterThrowsExceptionWhenNoMatchingStrategyExists()
        {
            var strategyCollection = mockFactory.Get(new Dictionary<string, IInlineConverterStrategy>()).Object;
            var logger = logMockFactory.Get<MarkdownInlineConverter>().Object;

            var converter = new MarkdownInlineConverter(strategyCollection, logger);

            var block = new Text();

            Assert.Throws<StrategyNotFoundException>(() => { converter.Convert(block); });
        }
        [HtmlConverterFor(nameof(Text))]
        private class TestConverter : IInlineConverterStrategy
        {
            public bool ConverterCalled = false;

            public string Convert(IInlineElement _)
            {
                ConverterCalled = true;
                return String.Empty;
            }
        }
    }
}
