using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class CopyFilesIntegrationTests : IntegrationTestBase
{
    [Fact]
    public void Test()
    {
        FileSystemCache.AddDirectory("input");
        FileSystemCache.AddFile("input/file1.txt", new MockFileData(string.Empty));
        FileSystemCache.AddFile("input/file2.txt", new MockFileData(string.Empty));
        FileSystemCache.AddDirectory("input/Folder1");
        FileSystemCache.AddFile("input/Folder1/file1.txt", new MockFileData(string.Empty));
        FileSystemCache.AddFile("input/Folder1/file2.txt", new MockFileData(string.Empty));
        FileSystemCache.AddDirectory("output");

        ServiceProvider.GetService<Generator>().Start();

        Assert.Contains("/output/file1.txt", FileSystemCache.AllFiles);
        Assert.Contains("/output/file2.txt", FileSystemCache.AllFiles);
        Assert.Contains("/output/Folder1/file1.txt", FileSystemCache.AllFiles);
        Assert.Contains("/output/Folder1/file2.txt", FileSystemCache.AllFiles);
    }
}
