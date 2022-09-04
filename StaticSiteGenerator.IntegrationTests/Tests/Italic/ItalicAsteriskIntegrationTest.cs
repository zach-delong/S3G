using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests.Italic;

public class ItalicAsteriskIntegrationTest : SimpleIntegrationTest
{
    protected override void Arrange()
    {
        var data = new [] {
	    ("templates/template/tag_templates/p.html", new MockFileData("<p class='testing'>{{}}</p>")),
	    ("templates/template/tag_templates/i.html", new MockFileData("<span class='testing'>{{}}</span>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("This is *italicized* text!"))
	};

        this.CreateFileSystemWith(data);
    }

    protected override void Act()
    {
        this.GenerateHtml();
    }

    protected override void Assert() {
        const string expectedContent = "<html><p class='testing'>This is <span class='testing'>italicized</span> text!</p></html>";
        const string expectedName = "/output/file1.html";

        this.AssertFilesExistWithContents(new []{
		(expectedName, expectedContent)
	});
    }

}
