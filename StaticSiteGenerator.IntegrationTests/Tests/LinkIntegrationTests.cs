using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class LinkIntegrationTests: SimpleIntegrationTest
{
    protected override void Arrange() {
        var data = new[] {
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
	    ("templates/template/tag_templates/link.html", new MockFileData("<a class='foo' href='{{url}}'>{{display_text}}</a>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("This is some [text!](https://www.google.com)"))
	};

        this.CreateFileSystemWith(data);
    }

    protected override void Act()
    {
        this.GenerateHtml();
    }

    protected override void Assert() {
        const string expectedFileContent = "<html><p>This is some <a class='foo' href='https://www.google.com\'>text!</a></p></html>";
        const string expectedFileName = "/output/file1.html";

        this.AssertFilesExistWithContents(new[]
	{
	    (expectedFileName, expectedFileContent)
	});
    }
}
