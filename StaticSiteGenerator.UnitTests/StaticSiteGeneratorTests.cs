using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.UnitTests.Doubles;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using StaticSiteGenerator.UnitTests.Helpers;
using Xunit;

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
            var mockFileIterator = FileIteratorMockFactory.Get(result);

            var mockFileParser = MarkdownFileParserMockFactory.Get(MarkdownFileContentsMocker.GetBlankBlockListForFilesWithNames(result));
            var mockMarkdownConverter = MarkdownConverterMockFactory.Get();
            var mockFileWriter = new Mock<IHtmlFileWriter>();

            var siteGenerator = new StaticSiteGenerator(
                mockFileIterator.Object,
                mockFileParser.Object,
                mockMarkdownConverter.Object,
                new CliOptions { OutputLocation="foo" },
                mockFileWriter.Object);

            siteGenerator.Start();

            mockFileIterator.Verify(m => m.GetFilesInDirectory(It.IsAny<string>()));
            mockFileParser.Verify(m => m.ReadFile(It.IsAny<string>()), Times.Exactly(numberOfFiles));
            mockMarkdownConverter.Verify(m => m.Convert(It.IsAny<IList<IBlockElement>>()), Times.Exactly(numberOfFiles));
            mockFileWriter.Verify(m => m.Write(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(numberOfFiles));
        }
    }
}
