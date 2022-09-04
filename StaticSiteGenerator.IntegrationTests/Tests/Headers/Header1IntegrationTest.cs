using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests.Headers;

public class Header1IntegrationTest : SimpleIntegrationTest
{
    protected override void Arrange() {
        var data = new[] {
	    ("templates/template/tag_templates/h1.html", new MockFileData("<h1 class='testing'>{{}}</h1>\n")),
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("# This is some text!"))
	};

        this.CreateFileSystemWith(data);
    }

    protected override void Act() {
        this.GenerateHtml();
    }

    protected override void Assert() {
        const string expectedFileContent = "<html><h1 class='testing'>This is some text!</h1>\n</html>";
        const string expectedFileName = "/output/file1.html";

        this.AssertFilesExistWithContents(new[] { (expectedFileName, expectedFileContent) });
    }
}
