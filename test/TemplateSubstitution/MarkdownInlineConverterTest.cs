using System;
using System.Collections.Generic;
using NUnit.Framework;

using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.InlineConverterStrategies;
using StaticSiteGenerator.Markdown.InlineElement;

namespace Test.TemplateSubstitution
{
    public class MarkdownInlineConverterTest
    {
        [Test]
        public void ConverterCallsCorrectStrategyWhenExists()
        {
            TestConverter testConverter = new TestConverter();
            var converter = new MarkdownInlineConverter(new List<IInlineConverterStrategy> {
                    testConverter
                        });

            var inline = new Text();

            converter.Convert(inline);

            Assert.That(testConverter.ConverterCalled, Is.True);
        }

        [Test]
        public void ConverterThrowsExceptionWhenNoMatchingStrategyExists()
        {
            var converter = new MarkdownInlineConverter(new List<IInlineConverterStrategy>());

            var block = new Text();

            Assert.Throws<Exception>(() => { converter.Convert(block); });
        }

        [HtmlConverterFor(nameof(Text))]
        private class TestConverter: IInlineConverterStrategy
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
