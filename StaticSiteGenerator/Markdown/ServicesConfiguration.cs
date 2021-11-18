using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.InlineElement;

namespace StaticSiteGenerator.Markdown
{
    public static class ServicesConfiguration
    {
        public static void AddMarkdownConverters(this IServiceCollection services)
        {
            services.AddTransient<IStrategy<IInline, IInlineElement>, TextElementConverter>();

            services.AddTransient<IBlockElementConverter, HeaderConverter>();
            services.AddTransient<IBlockElementConverter, ParagraphConverter>();
        }

        public static void AddMarkdownParsers(this IServiceCollection services)
        {
            services.AddTransient<IMarkdownBlockParser, MarkdownBlockParser>();

            services.AddTransient<IMarkdownFileParser, MarkdownFileParser>();
        }
    }
}
