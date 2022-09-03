using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class HeaderIntegrationTests : IntegrationTestBase
{
    [Fact]
    public void Header5ShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/h5.html", new MockFileData("<h5 class='testing'>{{}}</h5>\n"));
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md", new MockFileData("##### This is some text!"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><h5 class='testing'>This is some text!</h5>\n</html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(expectedFileContent, this.ReadFileContents(expectedFileName));
    }

    [Fact]
    public void Header6ShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/h6.html", new MockFileData("<h6 class='testing'>{{}}</h6>\n"));
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md", new MockFileData("###### This is some text!"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><h6 class='testing'>This is some text!</h6>\n</html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(expectedFileContent, this.ReadFileContents(expectedFileName));
    }

    // TODO: This test documents undesirable behavior but is the way it works for now
    [Fact]
    public void Header7ParsesAsParagraph()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/h6.html", new MockFileData("<h6 class='testing'>{{}}</h6>\n"));
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md", new MockFileData("####### This is some text!"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><p>####### This is some text!</p></html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(expectedFileContent, this.ReadFileContents(expectedFileName));
    }
}
