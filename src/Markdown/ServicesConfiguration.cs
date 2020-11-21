using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Markdown.InlineElementConverter;

namespace StaticSiteGenerator.Markdown
{
    public static class ServicesConfiguration
    {
        public static void AddMarkdownConverters(this IServiceCollection services)
        {
            var inlineConverters = new List<IInlineElementConverter> {
                new TextElementConverter()
            };

            services.AddSingleton<IList<IInlineElementConverter>>(inlineConverters);
        }
    }
}
