using System;
using System.Collections.Generic;
using Xunit;

using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;
using StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies;
using StaticSiteGenerator.Markdown.BlockElement;
using Test.Markdown.Parser;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace Test.TemplateSubstitution
{
    public class MarkdownBlockConverterTest
    {
        StrategyCollectionMockFactory mockFactory => new StrategyCollectionMockFactory();
        [Fact]
        public void ConverterCallsCorrectStrategyWhenExists()
        {
            TestConverter testConverter = new TestConverter();
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
        public void ConverterThrowsExceptionWhenNoMatchingStrategyExists()
        {
            var converter = new MarkdownBlockConverter(mockFactory.Get(new Dictionary<string, IBlockHtmlConverterStrategy>()).Object);

            var block = new Header();

            Assert.Throws<StrategyNotFoundException>(() => { converter.Convert(block); });
        }

        [HtmlConverterFor(nameof(Header))]
        private class TestConverter: IBlockHtmlConverterStrategy
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
