using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class CopyFilesIntegrationTest : SimpleIntegrationTest
{
    protected override void Arrange()
    {
	var data = new[]
	{
	    ("input/file1.txt", new MockFileData(string.Empty)),
	    ("input/file2.txt", new MockFileData(string.Empty)),
	    ("input/Folder1/file1.txt", new MockFileData(string.Empty)),
	    ("input/Folder1/file2.txt", new MockFileData(string.Empty)),
	    ("output", null)
	};

	this.CreateFileSystemWith(data);
    }
    protected override void Act()
    {
	this.GenerateHtml();
    }
    protected override void Assert()
    {
	this.AssertFilesExistWithContents(new[] {
	("/output/file1.txt", string.Empty),
	("/output/file2.txt", string.Empty)
	});
    }
}
