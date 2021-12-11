using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StaticSiteGenerator.IntegrationTests;

public static class ServicesConfiguration
{
    public static void OverrideFileReadingLayerWithDictionary(this IServiceCollection services,
                                                              MockFileSystem fileDictionary)
    {
        services.Remove(services.First(desc => desc.ServiceType == typeof(IFileSystem)));
        services.AddSingleton<IFileSystem>(fileDictionary);

        services.AddLogging(builder => builder.ClearProviders());
    }

    public static void MockCliOptions(this IServiceCollection services,
                                      string templateName,
                                      string markdownFileDirectoryName,
                                      string pathToOutput)
    {

        var cliOptions = new CliOptions()
        {
            TemplateName = templateName,
            PathToMarkdownFiles = markdownFileDirectoryName,
            OutputLocation = pathToOutput
        };

        services.AddSingleton(cliOptions);
    }
}
