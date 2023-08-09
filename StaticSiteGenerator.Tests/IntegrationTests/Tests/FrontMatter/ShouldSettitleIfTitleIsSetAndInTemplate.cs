using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests.Tests;

[Trait("Category", "Test")]
public class ShuldSettitleIfTitleIsSetAndInTemplate: SimpleIntegrationTest
{
    protected override void Arrange() {
        var inputFileContents = new MockFileData(@"---
title: stuff and things
published: true
---
Hello, world!");

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
        const string expectedFileContent = "<html><title>stuff and things</title><p>Hello, world!</p></html>";
        const string expectedFileName = "/output/file1.html";

        this.AssertFilesExistWithContents(new[] { (expectedFileName, expectedFileContent) });
    }
}
