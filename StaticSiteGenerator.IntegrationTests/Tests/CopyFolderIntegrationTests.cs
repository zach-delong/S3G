using System;
using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.IntegrationTests.Utilities;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class CopyFolderIntegrationTests : SimpleIntegrationTest
{
    protected override void Arrange()
    {
	this.CreateFileSystemWith(new[] {
	    ("templates/template/tag_templates/h1.html", new MockFileData("<h1>{{}}</h1>")),
	    ("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>")),
	    ("templates/template/site_template.html", new MockFileData("<html>{{}}</html>")),
	    ("output", null),
	    ("input/Folder1", null),
	    ("input/Folder1/SubFolder1", null),
	    ("input/Folder1/SubFolder2", null),
	    ("input/Folder2", null),
	});
    }
    protected override void Act()
    {
	this.GenerateHtml();
    }
    protected override void Assert()
    {
        var ps = new [] {
	    "/output/Folder1/SubFolder1", 
	    "/output/Folder1/SubFolder2", 
	    "/output/Folder2", 
	};

        this.AssertFoldersExist(ps);
    }
}
