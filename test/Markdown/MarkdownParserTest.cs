using System;
using NUnit.Framework;

using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;

//namespace Test.Markdown
 //    {
 //        [Test]
 //        [Parallelizable(ParallelScope.Self)]
 //        public void HeaderParses()
 //        {
 //            var input = @"# Header Text";
 //
 //            var parser = GetMarkdownParser();
 //
 //            var result = parser.Parse(input);
 //
 //            Console.WriteLine(result);
 //
 //            CollectionAssert.IsNotEmpty(result);
 //            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Header));
 //        }
 //
 //        private MarkdownParser GetMarkdownParser()
 //        {
 //            return new MarkdownParser(
 //                new HeaderConverter(),
 //                new ParagraphConverter()
 //            );
 //        }
 //
 //        [Test]
 //        [Parallelizable(ParallelScope.Self)]
 //        public void ParagraphParses()
 //        {
 //            var input = @"Paragraph Text";
 //
 //            var parser = GetMarkdownParser();
 //
 //            var result = parser.Parse(input);
 //
 //            Console.WriteLine(result);
 //
 //            CollectionAssert.IsNotEmpty(result);
 //            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Paragraph));
 //        }
 //
 //        [Test]
 //        [Parallelizable(ParallelScope.Self)]
 //        public void BlankParse()
 //        {
 //            var parser = GetMarkdownParser();
 //
 //            var result = parser.Parse(String.Empty);
 //
 //            CollectionAssert.IsEmpty(result);
 //        }
 //    }
 //}
