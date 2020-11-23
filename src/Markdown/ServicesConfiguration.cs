using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using StaticSiteGenerator.Markdown.BlockElementConverter;

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

            var blockConverters = new List<IBlockElementConverter>{
                new HeaderConverter();
            }
        }

        public static void AddMarkdownParsers(this IServiceCollection services)
        {
            services.AddTransient<IMarkdownInlineParser, MarkdownInlineParser>();
            services.AddTransient<IMarkdownParser, MarkdownParser>();
        }
    }
}
