using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Moq;
using StaticSiteGenerator.FileProcessingStrategies;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.Tests.Assertions;
using StaticSiteGenerator.UnitTests.Doubles.Markdown;
using Xunit;

namespace StaticSiteGenerator.UnitTests.FileProcessingStrategies;

public class MarkdownFileProcessingStrategyTests
{
    [Fact]
    public void Published_html_file_should_be_written()
    {
        var fs = new MockFileSystem();
        var mockFileParser = MarkdownFileParserMockFactory.Get(new Dictionary<string, IHtmlFile>
            {
                {"/input/foomd.md", new HtmlFile { Name = "output/foo", HtmlContent = "<h1>Hello<h1>", IsPublished = true }}
            });

        var mockHtmlFileWriter = new Mock<IHtmlFileWriter>();
        mockHtmlFileWriter.Setup(w => w.Write(It.IsAny<string>(), It.IsAny<string>()))
                          .Callback<string, string>((path, contents) => fs.AddFile(path, contents));

        var mockSiteTemplateFiller = new Mock<ISiteTemplateFiller>();
        mockSiteTemplateFiller.Setup(f => f.FillSiteTemplate(It.IsAny<IHtmlFile>()))
                              .Returns("<html><h1>Hello</h1></html>");

        var cliOptions = new CliOptions
        {
            PathToMarkdownFiles = "input",
            OutputLocation = "output"
        };

        var sut = new MarkdownFileProcessingStrategy(
            mockFileParser.Object,
            mockHtmlFileWriter.Object,
            mockSiteTemplateFiller.Object,
            cliOptions,
            fs
        );

        sut.Execute(new StaticSiteGenerator.Files.MarkdownFileSystemObject("/input/foomd.md"));

        var file = fs.GetFile("output/foomd.html");

        file.TextContents.Should().Be("<html><h1>Hello</h1></html>");
    }

    [Fact]
    public void Unpublished_html_file_should_not_be_written()
    {
        var fs = new MockFileSystem();
        var mockFileParser = MarkdownFileParserMockFactory.Get(new Dictionary<string, IHtmlFile>
            {
                {"/input/foomd.md", new HtmlFile { Name = "output/foo", HtmlContent = "<h1>Hello<h1>", IsPublished = false}}
            });

        var mockHtmlFileWriter = new Mock<IHtmlFileWriter>();
        mockHtmlFileWriter.Setup(w => w.Write(It.IsAny<string>(), It.IsAny<string>()))
                          .Callback<string, string>((path, contents) => fs.AddFile(path, contents));

        var mockSiteTemplateFiller = new Mock<ISiteTemplateFiller>();
        mockSiteTemplateFiller.Setup(f => f.FillSiteTemplate(It.IsAny<IHtmlFile>()))
                              .Returns("<html><h1>Hello</h1></html>");

        var cliOptions = new CliOptions
        {
            PathToMarkdownFiles = "input",
            OutputLocation = "output"
        };

        var sut = new MarkdownFileProcessingStrategy(
            mockFileParser.Object,
            mockHtmlFileWriter.Object,
            mockSiteTemplateFiller.Object,
            cliOptions,
            fs
        );

        sut.Execute(new StaticSiteGenerator.Files.MarkdownFileSystemObject("/input/foomd.md"));

        fs.Should().NotContainFile("output/foomd.html");
    }
}
