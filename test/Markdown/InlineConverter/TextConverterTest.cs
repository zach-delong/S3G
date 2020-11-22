using NUnit.Framework;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;

using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;

namespace Test.Markdown.ImnlineConverter
{
    class  TextConverterTest
    {
        [TestCase("sample text")]
        [TestCase("")]
        [Parallelizable(ParallelScope.Self)]
        public void TestTextConverter(string markdownInput)
        {
            var inline = new TextRunInline() { Text=markdownInput };
            var textConverter = new TextElementConverter();

            var result = textConverter.Convert(inline);

            Assert.That(result.Content, Is.EqualTo(markdownInput));
        }
    }
}
