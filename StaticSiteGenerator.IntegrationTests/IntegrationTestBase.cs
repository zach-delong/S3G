using System;
using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.IntegrationTests;

public abstract class IntegrationTestBase
{
    protected MockFileSystem FileSystemCache = new MockFileSystem();

    private IServiceProvider provider;
    public IServiceProvider ServiceProvider => provider ??= GetNewServiceProvider();

    private IServiceProvider GetNewServiceProvider()
    {
        ServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AddCustomServices();
        serviceCollection.OverrideFileReadingLayerWithDictionary(FileSystemCache);
        serviceCollection.MockCliOptions("template", "input", "output");

        return serviceCollection.BuildServiceProvider();
    }
}
