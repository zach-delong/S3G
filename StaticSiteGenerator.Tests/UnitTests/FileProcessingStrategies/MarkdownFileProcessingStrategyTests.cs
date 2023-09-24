using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.FileSystem;
using NSubstitute;
using NSubstitute.Extensions;
using StaticSiteGenerator.FileProcessingStrategies;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.Tests.AutoFixture;
using StaticSiteGenerator.Tests.UnitTests.Doubles;
using StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;
using StaticSiteGenerator.Tests.UnitTests.Doubles.HtmlWriting;
using StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.FileProcessingStrategies;

public class MarkdownFileProcessingStrategyTests: MockingTestBase
{

    Dictionary<string, IHtmlFile> fileSystemWithHtmlFile = new Dictionary<string, IHtmlFile>
    {
	{
	    "/input/foomd.md",
	    new HtmlFile
	    {
		Name = "output/foo",
		HtmlContent = "<h1>Hello<h1>",
		IsPublished = true
	    }
	}
    };

    [Fact]
    [Trait("Test", "true")]
    public void Published_html_file_should_be_written()
    {
        var fs = Mocker.MockFileSystem(new string[] { "/input/foomd.md" });
        Mocker.MockFileParser(fileSystemWithHtmlFile);
        Mocker.SetupHtmlfileWriter(fs);
        Mocker.SetupSiteTemplateFiller("<html><h1>Hello</h1></html>");
        Mocker.SetupCliOptions(pathToMarkdownFiles: "/input", outputLocation: "/output");

        var sut = Mocker.Create<MarkdownFileProcessingStrategy>();

        sut.Execute(new StaticSiteGenerator.Files.MarkdownFileSystemObject("/input/foomd.md"));

        var file = fs.GetFile("output/foomd.html");

        file.TextContents.Should().Be("<html><h1>Hello</h1></html>");
    }

    [Fact]
    public void Unpublished_html_file_should_not_be_written()
    {
        var fs = new MockFileSystem();

        Mocker.MockFileParser(new Dictionary<string, IHtmlFile>
            {
                {"/input/foomd.md", new HtmlFile { Name = "output/foo", HtmlContent = "<h1>Hello<h1>", IsPublished = false}}
            });
        Mocker.SetupHtmlfileWriter(fs);
        Mocker.SetupSiteTemplateFiller("<html><h1>Hello</h1></html>");
        Mocker.SetupCliOptions(pathToMarkdownFiles: "input", outputLocation: "output");

        var sut = Mocker.Create<MarkdownFileProcessingStrategy>();

        sut.Execute(new StaticSiteGenerator.Files.MarkdownFileSystemObject("/input/foomd.md"));

	MockFileSystemExtensions
	    .Should(fs)
	    .NotContainFile("output/foomd.html");
    }
}
