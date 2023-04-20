using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.Tests.IntegrationTests.Tests.CodeBlocks;

public class SimpleCodeBlockTest : SimpleIntegrationTest
{
    protected override void Arrange()
    {
        var inputFileContents = new MockFileData(@"
this is some code:

```sh

mkdir test-dir

```

");

        var data = new[]
	{
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
	    ("templates/template/site_template.html", new MockFileData("<html><title>{{title}}</title>{{}}</html>")),
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
        this.AssertFileExistsWithContents("/output/file1.html", "<html><title></title><p>this is some code:<code></code></p></html>");
    }
}
