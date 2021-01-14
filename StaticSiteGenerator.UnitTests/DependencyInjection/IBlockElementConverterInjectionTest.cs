using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElementConverter;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace StaticSiteGenerator.UnitTests.DependencyInjection
{
    public class IBlockElementConverterInjectionTest
    {
        [Fact]
        public void DiShouldReturnAllBlockElementConverters()
        {
            var diContainer = Program.BuildDependencies(new CliOptions());

            var converters = diContainer
                .GetService<IEnumerable<IBlockElementConverter>>()
                .ToDictionary(c => c.GetType(), c => c);

            Assert.NotNull(converters[typeof(HeaderConverter)]);
            Assert.NotNull(converters[typeof(ParagraphConverter)]);

            Assert.Collection(converters,
                              element => Assert.NotNull(element),
                              element => Assert.NotNull(element));
        }
    }
}
