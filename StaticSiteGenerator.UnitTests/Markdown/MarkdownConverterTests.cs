using System;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown;
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

        public void ConverterShouldNotEnumerate()
        {
            throw new NotImplementedException();
        }

        public void ConvertershouldEnumerateWhenManuallyEnumerated()
        {
            throw new NotImplementedException();
        }
    }
}
