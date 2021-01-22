
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies;
using StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;
using StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters;

namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public static class ServicesConfiguration
    {
        public static void AddHtmlConverters(this IServiceCollection services)
        {
            services.AddTransient<IBlockHtmlConverterStrategy, HeaderHtmlConverterStrategy>();
            services.AddTransient<IBlockHtmlConverterStrategy, ParagraphHtmlConverterStrategy>();

            services.AddTransient<IInlineConverterStrategy, TextConverter>();

            services.AddTransient<IMarkdownBlockConverter, MarkdownBlockConverter>();
            services.AddTransient<IMarkdownInlineConverter, MarkdownInlineConverter>();

            services.AddTransient<IMarkdownConverter, MarkdownConverter>();
        }

    }
}
