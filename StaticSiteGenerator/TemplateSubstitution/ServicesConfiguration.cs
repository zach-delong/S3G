using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies;
using StaticSiteGenerator.TemplateSubstitution.InlineConverterStrategies;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public static class ServicesConfiguration
    {
        public static void AddHtmlConverters(this IServiceCollection services)
        {
            services.AddTransient<IBlockHtmlConverterStrategy, HeaderConverterStrategy>();
            services.AddTransient<IBlockHtmlConverterStrategy, ParagraphConverterStrategy>();

            services.AddTransient<IInlineConverterStrategy, TextConverter>();

            services.AddTransient<IMarkdownBlockConverter, MarkdownBlockConverter>();
            services.AddTransient<IMarkdownInlineConverter, MarkdownInlineConverter>();

            services.AddSingleton<ITemplateReader, TemplateReader>();
        }

    }
}
