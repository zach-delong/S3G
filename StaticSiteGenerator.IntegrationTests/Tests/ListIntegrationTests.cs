using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.IntegrationTests.Utilities;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class ListIntegrationTests: IntegrationTestBase
{
    [Fact]
    public void UnorderedListsShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/p.html",
                                new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/tag_templates/ul.html",
                                new MockFileData("<ul>{{}}</ul>"));
        FileSystemCache.AddFile("templates/template/tag_templates/li.html",
                                new MockFileData("<li>{{}}</li>"));
        FileSystemCache.AddFile("templates/template/site_template.html",
                                new MockFileData("<html>{{}}</html>"));

        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md",
                                new MockFileData(@"
- Item 1
- Item 2
- Item 3"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><ul><li><p>Item 1</p></li><li><p>Item 2</p></li><li><p>Item 3</p></li></ul></html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(
            expectedFileContent,
            this.ReadFileContents(expectedFileName),
            ignoreCase: true);
    }

    [Fact]
    public void OrderedListsShouldParseCorrectly()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/p.html",
                                new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/tag_templates/ol.html",
                                new MockFileData("<ol>{{}}</ol>"));
        FileSystemCache.AddFile("templates/template/tag_templates/li.html",
                                new MockFileData("<li>{{}}</li>"));
        FileSystemCache.AddFile("templates/template/site_template.html",
                                new MockFileData("<html>{{}}</html>"));

        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddFile("input/file1.md",
                                new MockFileData(@"
1. Item 1
2. Item 2
3. Item 3"));

        ServiceProvider.GetService<Generator>().Start();

        const string expectedFileContent = "<html><ol><li><p>Item 1</p></li><li><p>Item 2</p></li><li><p>Item 3</p></li></ol></html>";
        const string expectedFileName = "/output/file1.html";

        Assert.True(this.FileExists(expectedFileName));
        Assert.Equal(
            expectedFileContent,
            this.ReadFileContents(expectedFileName),
            ignoreCase: true);
    }
}
