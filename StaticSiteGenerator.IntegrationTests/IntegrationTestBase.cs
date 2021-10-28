using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        protected IDictionary<string, MockFileData> FileSystemCache = new Dictionary<string, MockFileData>();

        private IServiceProvider provider;
        public IServiceProvider ServiceProvider
        {
            get
            {
                return provider ??=  GetNewServiceProvider();
            }
        }

        private IServiceProvider GetNewServiceProvider()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddCustomServices();
            serviceCollection.OverrideFileReadingLayerWithDictionary(FileSystemCache);
            serviceCollection.MockCliOptions("template", "input", "output");

            return serviceCollection.BuildServiceProvider();
        }
    }
}
