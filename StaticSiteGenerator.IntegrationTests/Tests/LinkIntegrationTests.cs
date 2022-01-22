using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.IntegrationTests.Utilities;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class LinkIntegrationTests: IntegrationTestBase
{
    [Fact]
    public void LinksShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/p.html",
                                new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/tag_templates/link.html",
                                new MockFileData("<a href='{{url}}'>{{display_text}}</a>"));
        FileSystemCache.AddFile("templates/template/site_template.html",
                                new MockFileData("<html>{{}}</html>"));

        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md",
                                new MockFileData("This is some [text!](https://www.google.com)"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><p>This is some <a href=\"https://www.google.com\">text!</a></p>\n</html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(
            expectedFileContent, 
            this.ReadFileContents(expectedFileName), 
            ignoreCase: true);
    }
}
