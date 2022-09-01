using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.DependencyInjection;
using XunitAssert = Xunit.Assert;

namespace StaticSiteGenerator.IntegrationTests.Utilities;

public static class IntegrationTestBaseTemplateMethods
{
    public static void CreateFileSystemWith(this IntegrationTestBase testBase, (string path, MockFileData content)[] fileSystemEntries)
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
    
    public static void AssertFilesExistWithContents(this IntegrationTestBase testBase, (string path, string contents)[] fileContents)
    {
        foreach (var (path, contents) in fileContents)
        {
	    XunitAssert.True(testBase.FileExists(path));
	    XunitAssert.Equal(contents, testBase.ReadFileContents(path));
        }
    }

    public static void AssertFoldersExist(this IntegrationTestBase testBase, string[] paths)
    {
        foreach (var path in paths)
        {
            XunitAssert.Contains(path, testBase.FileSystemCache.AllDirectories);
        }
    }
}
