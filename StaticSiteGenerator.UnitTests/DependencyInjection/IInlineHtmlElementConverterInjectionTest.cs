using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;

namespace StaticSiteGenerator.UnitTests.DependencyInjection
{
    public class IInlineHtmlElementConverterInjectionTest
    {
        [Fact]
        public void DishouldReturnHtmlElementConverterTest()
        {
            var diContainer = Program.BuildDependencies(new CliOptions());

            var converters = diContainer
                .GetService<IEnumerable<IInlineConverterStrategy>>()
                .ToDictionary(c => c.GetType(), c => c);

            Assert.NotNull(converters[typeof(TextConverter)]);

            Assert.Single(converters);
        }
    }
}
