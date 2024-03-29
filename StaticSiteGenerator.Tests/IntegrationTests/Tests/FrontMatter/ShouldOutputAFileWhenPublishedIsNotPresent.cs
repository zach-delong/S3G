using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class ShouldOutputAFileWhenPublishedIsNotPresent: SimpleIntegrationTest
{
    protected override void Arrange() {
        var inputFileContents = new MockFileData(@"---
title: stuff and things
---
Hello, world!");

        var data = new[]
	{
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
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
        const string expectedFileName = "/output/file1.html";
        this.AssertFileExists(expectedFileName);
    }
}
