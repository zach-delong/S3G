using System;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.CLI;

namespace StaticSiteGenerator;

internal static class ServiceCollectionFactory
{
    public static IServiceProvider Get(CliOptions options)
    {
        var service = new ServiceCollection();

        service.AddSingleton<MarkdownFilePathOption>(options);
        service.AddSingleton<TemplatePathOption>(options);
        service.AddSingleton<OutputLocationOption>(options);

        service.AddCustomServices();

        return service.BuildServiceProvider();
    }
}
