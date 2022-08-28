using Xunit;
using XunitAssert = Xunit.Assert;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests.Bold;

public class BoldAsteriskIntegrationTests: SimpleIntegrationTest
{
    protected override void Arrange()
    {
	this.CreateFileSystemWith(new[]
	{
	    ("templates/template/tag_templates/p.html", new MockFileData("<p class='testing'>{{}}</p>")),
	    ("templates/template/tag_templates/b.html", new MockFileData("<span class='testing'>{{}}</span>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("This is **bold** text!"))
	});
    }

    protected override void Act()
    {
        this.GenerateHtml();
    }

    protected override void Assert()
    {
        this.AssertFilesExistWithContents(new[] {
		(path: "/output/file1.html", contents: "<html><p class='testing'>This is <span class='testing'>bold</span> text!</p></html>")
	});
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

	XunitAssert.True(this.FileExists(expectedName));
	XunitAssert.Equal(expectedContent, this.ReadFileContents(expectedName));
    }
}
