using System.Collections.Generic;
using System.Linq;
using Moq;
using StaticSiteGenerator.Files.FileListing;
using StaticSiteGenerator.TemplateReading;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using Xunit;

namespace StaticSiteGenerator.UnitTests.TemplateSubstitution.TemplateReading
{
    public class ITemplateReaderTests
    {

        List<string> listOfFiles = new List<string>
        {
                "templates/tag_templates/h1.html",
                "templates/tag_templates/p.html"
        };


        Dictionary<string, string> fileContents = new Dictionary<string, string>
        {
            {"templates/tag_templates/h1.html", "h1 test content"},
            {"templates/tag_templates/p.html", "p test content"}
        };

        [Fact]
        public void TemplateReaderShouldLoadWhenDirectoryIsNotEmpty()
        {

            var reader = GetReader(listOfFiles, fileContents);

            // To list so that the enumerable actually enumerates
            var result = reader.ReadTemplate().ToList();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void TemplateReaerShouldLoadWhenFileContentsIsEmpty()
        {
            var reader = GetReader(new List<string> { "templates/tag_templates/h1.html" },
                                   new Dictionary<string, string> { { "templates/tag_templates/h1.html", "" } });

            var result = reader.ReadTemplate();

            foreach(var r in result)
            {
                System.Console.WriteLine(r);
            }

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
            Mock<IDirectoryEnumerator> directoryEnumeratorMock = DirectoryEnumeratorMockFactory.Get(listOfFiles);

            var fileReaderMock = FileReaderMockFactory.Get(fileNameToContents);

            var mockOptions = new Mock<CliOptions>();

            return new TemplateReader(directoryEnumeratorMock.Object,
                                      fileReaderMock.Object,
                                      mockOptions.Object);
        }

    }
}
