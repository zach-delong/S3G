using System;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator;

internal static class ServiceCollectionFactory
{
    public static IServiceProvider Get(CliOptions options)
    {
        var service = new ServiceCollection();
        service.AddCustomServices();

        service.AddSingleton(options);

        return service.BuildServiceProvider();
    }
}
