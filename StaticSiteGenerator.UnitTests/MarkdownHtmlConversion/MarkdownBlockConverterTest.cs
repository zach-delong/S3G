using System;
using System.Collections.Generic;
using Xunit;
using StaticSiteGenerator.Markdown.BlockElement;
using Test.Markdown.Parser;
using StaticSiteGenerator.Utilities.StrategyPattern;
using StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies;
using StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters;
using StaticSiteGenerator.MarkdownHtmlConversion;

namespace Test.MarkdownHtmlConversion
{
    public class MarkdownBlockConverterTest
    {
        StrategyCollectionMockFactory mockFactory => new StrategyCollectionMockFactory();

        [Fact]
        public void ConverterCallsCorrectStrategyWhenExists()
        {
            TestHeaderConverter testConverter = new TestHeaderConverter();
            var dict = new Dictionary<string, IBlockHtmlConverterStrategy> {
                { nameof(Header), testConverter }
            };
            var mock = mockFactory.Get<IBlockHtmlConverterStrategy>(dict);
            var converter = new MarkdownBlockConverter(mock.Object);

            var block = new Header();

            converter.Convert(block);

            Assert.True(testConverter.ConverterCalled);
        }

        [Fact]
        public void ConverterWorksOnLists()
        {
            TestHeaderConverter testConverter = new TestHeaderConverter();
            TestParagraphConverter testPConverter = new TestParagraphConverter();

            var dict = new Dictionary<string, IBlockHtmlConverterStrategy> {
                { nameof(Header), testConverter },
                { nameof(Paragraph), testPConverter }
            };

            var mock = mockFactory.Get<IBlockHtmlConverterStrategy>(dict);
            var converter = new MarkdownBlockConverter(mock.Object);

            var blocks = new List<IBlockElement> {
                new Header(),
                new Paragraph()
            };


            converter.Convert(blocks);

            Assert.True(testConverter.ConverterCalled);
            Assert.True(testPConverter.ConverterCalled);
        }

        [Fact]
        public void ConverterThrowsExceptionWhenNoMatchingStrategyExists()
        {
            var converter = new MarkdownBlockConverter(mockFactory.Get(new Dictionary<string, IBlockHtmlConverterStrategy>()).Object);

            var block = new Header();

            Assert.Throws<StrategyNotFoundException>(() => { converter.Convert(block); });
        }

        [HtmlConverterFor(nameof(Header))]
        private class TestHeaderConverter: IBlockHtmlConverterStrategy
         {
             public bool ConverterCalled = false;

             public string Convert(IBlockElement _)
             {
                 ConverterCalled = true;
                 return String.Empty;
             }
         }

        [HtmlConverterFor(nameof(Paragraph))]
        private class TestParagraphConverter : IBlockHtmlConverterStrategy
        {
            public bool ConverterCalled = false;

            public string Convert(IBlockElement _)
            {
                ConverterCalled = true;
                return String.Empty;
            }
        }
      }
}
