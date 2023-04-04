using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests.Bold;

public class BoldUnderscoreIntegrationTests: SimpleIntegrationTest
{
    protected override void Arrange()
    {
	this.CreateFileSystemWith(new[]
	{
	    ("templates/template/tag_templates/p.html", new MockFileData("<p class='testing'>{{}}</p>")),
	    ("templates/template/tag_templates/b.html", new MockFileData("<span class='testing'>{{}}</span>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("This is __bold__ text!")),
	});
    }

    protected override void Act()
    {
	this.GenerateHtml();
    }

    protected override void Assert()
    {
	this.AssertFilesExistWithContents(new[] {
	    (path: "/output/file1.html",
	     contents: "<html><p class='testing'>This is <span class='testing'>bold</span> text!</p></html>")
	});
    }
}
