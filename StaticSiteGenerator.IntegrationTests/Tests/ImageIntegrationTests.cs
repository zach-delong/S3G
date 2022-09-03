using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class ImageIntegrationTests: SimpleIntegrationTest
{
    protected override void Arrange() {
	var data = new [] {
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
	    ("templates/template/tag_templates/image.html", new MockFileData("<img href='{{url}}' />")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("![dummy](img/image.png)")),
	};

        this.CreateFileSystemWith(data);
    }

    protected override void Act() {
        this.GenerateHtml();
    }

    protected override void Assert() {
        const string expectedFileContent = "<html><p><img href='img/image.png' /></p></html>";
        const string expectedFileName = "/output/file1.html";
        this.AssertFilesExistWithContents(new[] { (expectedFileName, expectedFileContent) });
    }
}
