using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.IntegrationTests.Utilities;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class ImageIntegrationTests: IntegrationTestBase
{
    [Fact]
    public void ImagesShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/p.html",
                                new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/tag_templates/image.html",
                                new MockFileData("<img href='{{url}}' />"));
        FileSystemCache.AddFile("templates/template/site_template.html",
                                new MockFileData("<html>{{}}</html>"));

        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md",
                                new MockFileData("![dummy](img/image.png)"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><p><img href='img/image.png' /></p></html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(
            expectedFileContent,
            this.ReadFileContents(expectedFileName),
            ignoreCase: true);
    }
}
