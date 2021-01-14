using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies;
using Xunit;

namespace StaticSiteGenerator.UnitTests.DependencyInjection
{
    public class IBlockHtmlConverterStrategyInjectionTest
    {
        public void IBlockHtmlElementConverterInjectionTest()
        {
            var diContainer = Program.BuildDependencies(new CliOptions());

            var converters = diContainer
                .GetService<IEnumerable<IBlockHtmlConverterStrategy>>()
                .ToDictionary(c => c.GetType(), c => c);

            Assert.NotNull(converters[typeof(HeaderHtmlConverterStrategy)]);
            Assert.NotNull(converters[typeof(ParagraphHtmlConverterStrategy)]);

            Assert.Collection(converters,
                              e => Assert.NotNull(e),
                              e => Assert.NotNull(e));
        }
    }
}
