using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class LocalFileLinkIntegrationTest: SimpleIntegrationTest
{
    protected override void Arrange() {
        var data = new[] {
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
	    ("templates/template/tag_templates/link.html", new MockFileData("<a class='foo' href='{{url}}'>{{display_text}}</a>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("This is some [text!](file2.md)")),
	    ("input/file2.md", new MockFileData("This is some [text!](file1.md)"))
	};

        this.CreateFileSystemWith(data);
    }

    protected override void Act()
    {
        this.GenerateHtml();
    }

    protected override void Assert() {
        const string expectedFileContent = "<html><p>This is some <a class='foo' href='file2.html'>text!</a></p></html>";
        const string expectedFileName = "/output/file1.html";

        this.AssertFilesExistWithContents(new[]
	{
	    (expectedFileName, expectedFileContent)
	});
    }
}
