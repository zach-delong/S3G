using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using StaticSiteGenerator.SiteTemplating.SiteTemplateReading;
using StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.SiteTemplating;

public class SiteTemplateLocalFileReaderTests
{
    [Fact]
    public void ShouldTryToReadFileAtRootOfTemplate()
    {
        // This test mostly just verifies that the reader is trying to read the correct file
        // And that it builds the correct path to the template;
        var fileCache = new Dictionary<string, string>
            {
                {Path.Combine("templates", "template", "site_template.html"), "fileContents"}
            };

        var readerMock = FileReaderMockFactory.Get(fileCache);

        var sut = new SiteTemplateLocalFileReader(readerMock.Object, new CliOptions { TemplatePath = "templates/template" });

        var result = sut.ReadTemplate();

	result.Should().BeEquivalentTo("fileContents");
    }
}
