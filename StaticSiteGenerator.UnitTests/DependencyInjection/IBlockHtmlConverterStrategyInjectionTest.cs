using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies;
using Xunit;

namespace StaticSiteGenerator.UnitTests.DependencyInjection
{
    public class IBlockHtmlConverterStrategyInjectionTest
    {
        [Fact]
        public void IBlockHtmlElementConverterInjectionTest()
        {
            var diContainer = Program.BuildDependencies(new CliOptions());

            var converters = diContainer
                .GetService<IEnumerable<IBlockHtmlConverterStrategy>>()
                .ToDictionary(c => c.GetType(), c => c);

            Assert.NotNull(converters[typeof(HeaderHtmlConverterStrategy)]);
            Assert.NotNull(converters[typeof(ParagraphHtmlConverterStrategy)]);

            Assert.Collection(converters,
                              e => Assert.NotNull(e.Value),
                              e => Assert.NotNull(e.Value));
        }
    }
}
