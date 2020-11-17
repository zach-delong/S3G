using System;
using NUnit.Framework;

using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.MarkdownElement;
using StaticSiteGenerator.Markdown.MarkdownElementConverter;

namespace Test.Markdown
{
    public class MarkdownParserTest
    {
        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void HeaderParses()
        {
            var input = @"# Header Text";

            var parser = GetMarkdownParser();

            var result = parser.Parse(input);

            Console.WriteLine(result);

            CollectionAssert.IsNotEmpty(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Header));
        }

        private MarkdownParser GetMarkdownParser()
        {
            return new MarkdownParser(new MarkdownHeaderElementConverter());
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void ParagraphParses()
        {
            throw new NotImplementedException();
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void BlankParse()
        {
            throw new NotImplementedException();
        }
    }
}