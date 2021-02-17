using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown
{
    public class MarkdownConverterTests
    {
        private MarkdownBlockConverterMockFactory MockFactory => new MarkdownBlockConverterMockFactory();

        [Theory]
        [InlineData("")]
        [InlineData("output")]
        [InlineData(null)]
        public void PathShouldBeAdjustedToOutputFolder(string outputLocation)
        {
            var cliOptions = new CliOptions
            {
                OutputLocation = outputLocation
            };

            var converter = new MarkdownConverter(MockFactory.Get().Object, cliOptions);

            var markdownFile = new MarkdownFile
            {
                Name = "test.md"
            };

            var result = converter.Convert(new List<IMarkdownFile> { markdownFile });

            // Since the null string can't be searched for, lets cast it to empty
            // (all strings contain the empty string, so this should just not throw an exception)
            Assert.All(result, f => f.Name.Contains(outputLocation ?? String.Empty));
        }

        [Fact]
        public void ConverterShouldNotEnumerate()
        {
            var cliOptions = new CliOptions();
            var mock = MockFactory.Get();

            var converter = new MarkdownConverter(mock.Object, cliOptions);

            var markdownFile = new MarkdownFile
            {
                Name = "test.md"
            };

            var result = converter.Convert(new List<IMarkdownFile> { markdownFile, markdownFile});

            mock.Verify(m => m.Convert(It.IsAny<IList<IBlockElement>>()), Times.Never());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(4)]
        public void ConvertershouldEnumerateWhenManuallyEnumerated(int numFiles)
        {
            var cliOptions = new CliOptions();
            var mock = MockFactory.Get();

            var converter = new MarkdownConverter(mock.Object, cliOptions);

            var markdownFile = new MarkdownFile
            {
                Name = "test.md"
            };

            var list = new List<IMarkdownFile>();

            for (var i = 0; i < numFiles; i++)
            {
                list.Add(markdownFile);
            }

            var result = converter.Convert(list)
                                  .ToList();

            mock.Verify(m => m.Convert(It.IsAny<IList<IBlockElement>>()), Times.Exactly(numFiles));
        }
    }
}
