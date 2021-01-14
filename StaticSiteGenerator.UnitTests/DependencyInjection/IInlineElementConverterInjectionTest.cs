using Xunit;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StaticSiteGenerator.UnitTests.DependencyInjection
{
    public class IInlineElementConverterInjectionTest
    {
        [Fact]
        public void DiShouldReturnAllInlineElementConverters()
        {
            var diContainer = Program.BuildDependencies(new CliOptions());

            var converters = diContainer
                .GetService<IEnumerable<IInlineElementConverter>>()
                .ToDictionary(c => c.GetType(), c => c);


            Assert.NotNull(converters[key: typeof(TextElementConverter)]);

            Assert.Single(converters);
        }
    }
}
