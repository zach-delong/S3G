using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.IntegrationTests.Utilities.FluentAssertionExtensions;

namespace StaticSiteGenerator.IntegrationTests.Utilities;

public static class IntegrationTestBaseTemplateMethods
{
    public static void CreateFileSystemWith(
	this IntegrationTestBase testBase,
	(string path, MockFileData content)[] fileSystemEntries)
    {
	foreach (var (path, content) in fileSystemEntries)
	{
	    if (content == null)
	    {
		testBase.FileSystemCache.AddDirectory(path);
	    }
	    else
	    {
		testBase.FileSystemCache.AddFile(path, content);
	    }
	}
    }

    public static void GenerateHtml(this IntegrationTestBase testBase)
    {
	testBase.ServiceProvider.GetService<Generator>().Start();
    }

    public static void AssertFilesExistWithContents(
	this IntegrationTestBase testBase,
	(string path, string contents)[] fileContents)
    {
        using (new AssertionScope())
        {
            foreach (var (path, contents) in fileContents)
            {
                testBase.FileSystemCache.Should()
                                        .FileExistsWithContents(path, contents);
            }
        }
    }

    public static void AssertFileDoesNotExist(
	this IntegrationTestBase testBase, string fileName)
    {
        testBase.FileSystemCache.Should()
                                .NotContainFile(fileName);
    }

    public static void AssertFileExists(this IntegrationTestBase testBase, string fileName)
    {
        testBase.FileSystemCache.Should()
                                .ContainFile(fileName);
    }

    public static void AssertFoldersExist(this IntegrationTestBase testBase, string[] paths)
    {
	foreach (var path in paths)
	{
	    testBase.FileSystemCache.AllDirectories.Should().Contain(path);
	}
    }
}
