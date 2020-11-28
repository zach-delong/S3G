using System;
using System.Collections.Generic;
using NUnit.Framework;

using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.Markdown.BlockElement;

namespace Test.TemplateSubstitution
{
    public class MarkdownBlockConverterTest
    {
        [Test]
        public void ConverterCallsCorrectStrategyWhenExists()
        {
            TestConverter testConverter = new TestConverter();
            var converter = new MarkdownBlockConverter(new List<IHtmlConverter<IBlockElement>> {
                    testConverter
                        });

            var block = new Header();

            converter.Convert(block);

            Assert.That(testConverter.ConverterCalled, Is.True);
        }

        [Test]
        public void ConverterThrowsExceptionWhenNoMatchingStrategyExists()
        {
            var converter = new MarkdownBlockConverter(new List<IHtmlConverter<IBlockElement>>());

            var block = new Header();

            Assert.Throws<Exception>(() => { converter.Convert(block); });
        }

        [HtmlConverterFor(nameof(Header))]
         private class TestConverter: IHtmlConverter<IBlockElement>
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
