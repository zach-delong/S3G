using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Moq;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.Files.FileProcessingStrategies;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Files.FileProcessingStrategies
{
    public class MarkdownFileProcessingStrategyTests
    {
        [Fact]
        public void Test()
        {
            var fs = new MockFileSystem();
            var mockFileParser = MarkdownFileParserMockFactory.Get(new Dictionary<string, IMarkdownFile>
            {
                {"/input/foomd.md", new StaticSiteGenerator.Markdown.MarkdownFile { Elements = new List<IBlockElement> { new Header { Level= 1, Text = "Hello" }}}}
            });

            var mockMarkdownConverter = new Mock<IMarkdownConverter>();
            mockMarkdownConverter.Setup(c => c.Convert(It.IsAny<IMarkdownFile>()))
                                 .Returns(new HtmlFile { Name = "output/foo", HtmlContent = "<h1>Hello<h1>"});

            var mockHtmlFileWriter = new Mock<IHtmlFileWriter>();
            mockHtmlFileWriter.Setup(w => w.Write(It.IsAny<string>(), It.IsAny<string>()))
                              .Callback<string, string>((path, contents) => fs.AddFile(path, contents));

            var mockSiteTemplateFiller = new Mock<ISiteTemplateFiller>();
            mockSiteTemplateFiller.Setup(f => f.FillSiteTemplate(It.IsAny<string>()))
                                  .Returns("<html><h1>Hello</h1></html>");

            var cliOptions = new CliOptions
            {
                PathToMarkdownFiles = "input",
                OutputLocation = "output"
            };

            var sut = new MarkdownFileProcessingStrategy(
                mockFileParser.Object,
                mockMarkdownConverter.Object,
                mockHtmlFileWriter.Object,
                mockSiteTemplateFiller.Object,
                cliOptions,
                fs
            );

            sut.Execute(new StaticSiteGenerator.Files.MarkdownFileSystemObject("/input/foomd.md"));


            var file = fs.GetFile("output/foomd.html");

            Assert.Equal("<html><h1>Hello</h1></html>", file.TextContents);
        }
    }
}