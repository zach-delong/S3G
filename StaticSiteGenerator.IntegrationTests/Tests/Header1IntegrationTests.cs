using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class Header1IntegrationTests : IntegrationTestBase
{
    [Fact]
    public void Header1ShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/h1.html", new MockFileData("<h1 class='testing'>{{}}</h1>\n"));
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md", new MockFileData("# This is some text!"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><h1 class='testing'>This is some text!</h1>\n</html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(expectedFileContent, this.ReadFileContents(expectedFileName));
    }
}
