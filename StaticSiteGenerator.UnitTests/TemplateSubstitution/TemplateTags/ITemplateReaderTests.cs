using System.Collections.Generic;
using System.Linq;
using Moq;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using Xunit;

namespace StaticSiteGenerator.UnitTests.TemplateSubstitution.TemplateTags
{
    public class ITemplateReaderTests
    {

        List<string> listOfFiles = new List<string>
        {
                "h1.html",
                "p.html"
        };


        Dictionary<string, string> fileContents = new Dictionary<string, string>
        {
            {"h1.html", "h1 test content"},
            {"p.html", "p test content"}
        };

        [Fact]
        public void TemplateReaderShouldLoadWhenDirectoryIsNotEmpty()
        {

            var reader = GetReader(listOfFiles, fileContents);

            var result = reader.ReadTemplate();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void TemplateReaerShouldLoadWhenFilecontentsIsEmpty()
        {
            var reader = GetReader(new List<string> { "h1.html" },
                                   new Dictionary<string, string> { { "h1.html", "" } });

            var result = reader.ReadTemplate();

            Assert.Empty(result.First(t => t.Type == TagType.Header1).Template);
        }

        [Fact]
        public void TemplateReaderShouldReturnEmptyList()
        {
            var reader = GetReader(new List<string>(),
                                   new Dictionary<string, string>());

            var result = reader.ReadTemplate();

            Assert.Empty(result);
        }

        private ITemplateReader GetReader(
            IEnumerable<string> listOfFiles,
            IDictionary<string, string> fileNameToContents)
        {
            Mock<FileIterator> fileIteratorMock = FileIteratorMockFactory.Get(listOfFiles);

            var fileReaderMock = FileReaderMockFactory.Get(fileNameToContents);

            var mockOptions = new Mock<CliOptions>();

            return new TemplateReader(fileIteratorMock.Object,
                                      fileReaderMock.Object,
                                      mockOptions.Object);
        }

    }
}
