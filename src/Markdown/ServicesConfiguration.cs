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
            services.AddTransient<IInlineElementConverter, TextElementConverter>();

            services.AddTransient<IBlockElementConverter, HeaderConverter>();
            services.AddTransient<IBlockElementConverter, ParagraphConverter>();
        }

        public static void AddMarkdownParsers(this IServiceCollection services)
        {
            services.AddTransient<IMarkdownInlineParser, MarkdownInlineParser>();
            services.AddTransient<IMarkdownParser, MarkdownParser>();
        }
    }
}
