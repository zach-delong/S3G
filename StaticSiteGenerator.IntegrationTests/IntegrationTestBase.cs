using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.IntegrationTests
{
    public class IntegrationTestBase
    {
        protected IDictionary<string, string> InputFileSystem = new Dictionary<string, string>();
        protected IDictionary<string, string> FileSystemWritingCache = new Dictionary<string, string>();

        protected IServiceProvider ServiceProvider
        {
            get
            {
                ServiceCollection serviceCollection = new ServiceCollection();

                serviceCollection.AddCustomServices();
                serviceCollection.OverrideFileReadingLayerWithDictionary(InputFileSystem);
                serviceCollection.MockFileWriter(FileSystemWritingCache);
                serviceCollection.MockCliOptions("template", "input", "output");

                return serviceCollection.BuildServiceProvider();
            }
        }

    }
}
