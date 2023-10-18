using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StaticSiteGenerator.CLI;
using StaticSiteGenerator.Tests.UnitTests.Doubles;

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
                                      string templatePath,
                                      string markdownFileDirectoryName,
                                      string pathToOutput)
    {

	var options = CliOptionsBuilder
	    .Get()
	    .WithOutputLocation(pathToOutput)
	    .WithPathToMarkdownFiles(markdownFileDirectoryName)
	    .WithTemplatePath(templatePath)
	    .Build();

        services.AddSingleton<MarkdownFilePathOption>(options);
        services.AddSingleton<TemplatePathOption>(options);
        services.AddSingleton<OutputLocationOption>(options);
    }
}
