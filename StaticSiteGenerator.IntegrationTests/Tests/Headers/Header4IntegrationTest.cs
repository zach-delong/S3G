using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests.Headers;

public class Header4IntegrationTest : SimpleIntegrationTest
{
    protected override void Arrange() {
        var data = new[] {
	    ("templates/template/tag_templates/h4.html", new MockFileData("<h4 class='testing'>{{}}</h4>\n")),
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("#### This is some text!"))
	};

        this.CreateFileSystemWith(data);
    }

    protected override void Act() {
        this.GenerateHtml();
    }

    protected override void Assert() {
        const string expectedFileContent = "<html><h4 class='testing'>This is some text!</h4>\n</html>";
        const string expectedFileName = "/output/file1.html";

        this.AssertFilesExistWithContents(new[] { (expectedFileName, expectedFileContent) });
    }
}
