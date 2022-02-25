using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class BoldIntegrationTests: IntegrationTestBase
{
    [Fact]
    public void AsterisksShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p class='testing'>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/tag_templates/b.html", new MockFileData("<span class='testing'>{{}}</span>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md", new MockFileData("This is **bold** text!"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedContent = "<html><p class='testing'>This is <span class='testing'>bold</span> text!</p></html>";
        const string expectedName = "/output/file1.html";

        Assert.True(this.FileExists(expectedName));
        Assert.Equal(expectedContent, this.ReadFileContents(expectedName));
    }

    [Fact]
    public void UnderscoreShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p class='testing'>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/tag_templates/b.html", new MockFileData("<span class='testing'>{{}}</span>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md", new MockFileData("This is __bold__ text!"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedContent = "<html><p class='testing'>This is <span class='testing'>bold</span> text!</p></html>";
        const string expectedName = "/output/file1.html";

        Assert.True(this.FileExists(expectedName));
        Assert.Equal(expectedContent, this.ReadFileContents(expectedName));
    }
}
