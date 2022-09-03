using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests.Headers;

// TODO: This test documents undesirable behavior but is the way it works for now
public class Header7IntegrationTest : SimpleIntegrationTest
{
    protected override void Arrange() {
        var data = new[] {
	    ("templates/template/tag_templates/h6.html", new MockFileData("<h6 class='testing'>{{}}</h6>\n")),
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("####### This is some text!"))
	};

        this.CreateFileSystemWith(data);
    }

    protected override void Act() {
        this.GenerateHtml();
    }

    protected override void Assert() {
        const string expectedFileContent = "<html><p>####### This is some text!</p></html>";
        const string expectedFileName = "/output/file1.html";

        this.AssertFilesExistWithContents(new[] { (expectedFileName, expectedFileContent) });
    }
}
