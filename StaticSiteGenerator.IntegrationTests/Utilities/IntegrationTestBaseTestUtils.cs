using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.IntegrationTests.Utilities;

public static class IntegrationTestBaseTestUtils
{
    public static bool FileExists(this IntegrationTestBase testBase, string pathToFile)
    {
        var fileSystem = testBase.ServiceProvider.GetService<IFileSystem>();

        return fileSystem.File.Exists(pathToFile);
    }

    public static string ReadFileContents(this IntegrationTestBase testBase, string pathToFile)
    {
        var fileSystem = testBase.ServiceProvider.GetService<IFileSystem>();

        return fileSystem.File.ReadAllText(pathToFile);
    }
}

