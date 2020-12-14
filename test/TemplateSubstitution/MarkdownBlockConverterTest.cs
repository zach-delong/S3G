using System;
using System.Collections.Generic;
using NUnit.Framework;

using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;
using StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies;
using StaticSiteGenerator.Markdown.BlockElement;

namespace Test.TemplateSubstitution
{
    public class MarkdownBlockConverterTest
    {
        [Test]
        public void ConverterCallsCorrectStrategyWhenExists()
        {
            TestConverter testConverter = new TestConverter();
            var converter = new MarkdownBlockConverter(new List<IBlockHtmlConverterStrategy> {
                    testConverter
                        });

            var block = new Header();

            converter.Convert(block);

            Assert.That(testConverter.ConverterCalled, Is.True);
        }

        [Test]
        public void ConverterThrowsExceptionWhenNoMatchingStrategyExists()
        {
            var converter = new MarkdownBlockConverter(new List<IBlockHtmlConverterStrategy>());

            var block = new Header();

            Assert.Throws<Exception>(() => { converter.Convert(block); });
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
