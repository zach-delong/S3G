using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests.Lists;

public class OrderedListsIntegrationTest: SimpleIntegrationTest
{
    protected override void Arrange()
    {
        var inputFileContents = new MockFileData(@"
1. Item 1
2. Item 2
3. Item 3");
	
        var data = new[]
	{
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
	    ("templates/template/tag_templates/ol.html", new MockFileData("<ol>{{}}</ol>")),
	    ("templates/template/tag_templates/li.html", new MockFileData("<li>{{}}</li>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/file1.md", inputFileContents)   
	};

        this.CreateFileSystemWith(data);
    }
    protected override void Act()
    {
        this.GenerateHtml();
    }

    protected override void Assert()
    {
        const string expectedFileContent = "<html><ol><li><p>Item 1</p></li><li><p>Item 2</p></li><li><p>Item 3</p></li></ol></html>";
        const string expectedFileName = "/output/file1.html";

        this.AssertFilesExistWithContents(new[] { (expectedFileName, expectedFileContent) });
    }
}
