using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.Markdown.Parser.InlineParser;

namespace StaticSiteGenerator.Markdown
{
    public static class ServicesConfiguration
    {
        public static void AddMarkdownConverters(this IServiceCollection services)
        {
            services.AddTransient<IInlineElementConverter, TextElementConverter>();

            services.AddTransient<IBlockElementConverter, HeaderConverter>();
            services.AddTransient<IBlockElementConverter, ParagraphConverter>();
            services.AddTransient<IBlockElementConverter, YamlConverter>();
        }

        public static void AddMarkdownParsers(this IServiceCollection services)
        {
            services.AddTransient<IMarkdownInlineParser, MarkdownInlineParser>();
            services.AddTransient<IMarkdownBlockParser, MarkdownBlockParser>();

            services.AddTransient<IMarkdownFileParser, MarkdownFileParser>();
        }
    }
}
