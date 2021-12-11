using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests.Tests;

public class CopyFolderIntegrationTests : IntegrationTestBase
{
    [Fact]
    public void ShouldCopySubDirectories()
    {
        FileSystemCache.AddFile("templates/template/tag_templates/h1.html", new MockFileData("<h1>{{}}</h1>"));
        FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
        FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
        FileSystemCache.AddDirectory("output");
        FileSystemCache.AddDirectory("input/Folder1");
        FileSystemCache.AddDirectory("input/Folder1/SubFolder1");
        FileSystemCache.AddDirectory("input/Folder1/SubFolder2");
        FileSystemCache.AddDirectory("input/Folder2");

        ServiceProvider.GetService<Generator>().Start();

        Assert.Contains("/output/Folder1/SubFolder1", FileSystemCache.AllDirectories);
        Assert.Contains("/output/Folder1/SubFolder2", FileSystemCache.AllDirectories);
        Assert.Contains("/output/Folder2", FileSystemCache.AllDirectories);
    }
}
