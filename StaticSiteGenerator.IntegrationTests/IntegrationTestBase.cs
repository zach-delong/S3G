using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.IntegrationTests
{
    public class IntegrationTestBase
    {
        protected IDictionary<string, string> FileSystemCache = new ConcurrentDictionary<string, string>();

        protected IServiceProvider ServiceProvider
        {
            get
            {
                ServiceCollection serviceCollection = new ServiceCollection();

                serviceCollection.AddCustomServices();
                serviceCollection.OverrideFileReadingLayerWithDictionary(FileSystemCache);
                serviceCollection.MockFileWriter(FileSystemCache);
                serviceCollection.MockCliOptions("template", "input", "output");

                return serviceCollection.BuildServiceProvider();
            }
        }

    }
}
