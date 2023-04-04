using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class ParagraphIntegrationTests: SimpleIntegrationTest
{
    protected override void Arrange()
    {
        var data = new[]
	{
	    ("templates/template/tag_templates/p.html", new MockFileData("<p class='testing'>{{}}</p>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", new MockFileData("This is some text!"))
	};

        this.CreateFileSystemWith(data);
    }

    protected override void Act()
    {
        this.GenerateHtml();
    }

    protected override void Assert()
    {
        const string expectedContent = "<html><p class='testing'>This is some text!</p></html>";
        const string expectedName = "/output/file1.html";

        this.AssertFilesExistWithContents(new[]
        {
	    (expectedName, expectedContent)
	});
    }
}
