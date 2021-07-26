using System.Collections.Generic;
using System.Linq;
using Moq;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using StaticSiteGenerator.UnitTests.Helpers;
using Xunit;
using Microsoft.Extensions.Logging;

namespace StaticSiteGenerator.UnitTests
{
    public class StaticSiteGeneratorTests
    {
        private RandomizedStringListGenerator FileNameGenerator => new RandomizedStringListGenerator();
        private BlankMarkdownFileDictionaryGenerator MarkdownFileContentsMocker => new BlankMarkdownFileDictionaryGenerator();

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(10)]
        public void TestSiteGeneratorRun(int numberOfFiles)
        {
            IEnumerable<string> result = FileNameGenerator.GetStrings(numberOfFiles).ToList();
            var mockFileIterator = DirectoryEnumeratorMockFactory.Get(result);
            var mockMarkdownConverter = MarkdownConverterMockFactory.Get();
            var mockFileWriter = new Mock<IHtmlFileWriter>();
            var mockSiteTemplater = new Mock<ISiteTemplateFiller>();
            var mockLogger = new Mock<ILogger<StaticSiteGenerator>>();

            mockSiteTemplater.Setup(m => m.FillSiteTemplate(It.IsAny<string>()))
                             .Returns<string>(s => s);


            var mockFileParser = MarkdownFileParserMockFactory.Get(MarkdownFileContentsMocker.GetBlankBlockListForFilesWithNames(result));
            var siteGenerator = new StaticSiteGenerator(
                mockFileIterator.Object,
                mockFileParser.Object,
                mockMarkdownConverter.Object,
                new CliOptions { OutputLocation = "foo" },
                mockFileWriter.Object,
                mockSiteTemplater.Object,
                mockLogger.Object);

            siteGenerator.Start();

            mockFileIterator.Verify(m => m.GetFiles(It.IsAny<string>(), "*.md"));
            mockFileParser.Verify(m => m.ReadFiles(It.IsAny<IEnumerable<string>>()));
            mockMarkdownConverter.Verify(m => m.Convert(It.IsAny<IEnumerable<IMarkdownFile>>()));
            mockFileWriter.Verify(m => m.Write(It.IsAny<IEnumerable<IHtmlFile>>()));
            mockSiteTemplater.Verify(m => m.FillSiteTemplate(It.IsAny<IEnumerable<IHtmlFile>>()));
        }
    }
}
