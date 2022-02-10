using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class HeaderIntegrationTests : IntegrationTestBase
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

    [Fact]
    public void Header2ShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/h2.html", new MockFileData("<h2 class='testing'>{{}}</h2>\n"));
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md", new MockFileData("## This is some text!"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><h2 class='testing'>This is some text!</h2>\n</html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(expectedFileContent, this.ReadFileContents(expectedFileName));
    }

    [Fact]
    public void Header3ShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/h3.html", new MockFileData("<h3 class='testing'>{{}}</h3>\n"));
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md", new MockFileData("### This is some text!"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><h3 class='testing'>This is some text!</h3>\n</html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(expectedFileContent, this.ReadFileContents(expectedFileName));
    }

    [Fact]
    public void Header4ShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/h4.html", new MockFileData("<h4 class='testing'>{{}}</h4>\n"));
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md", new MockFileData("#### This is some text!"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><h4 class='testing'>This is some text!</h4>\n</html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(expectedFileContent, this.ReadFileContents(expectedFileName));
    }

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
