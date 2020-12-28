using Microsoft.Extensions.DependencyInjection;

using StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies;
using StaticSiteGenerator.TemplateSubstitution.InlineConverterStrategies;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

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

            services.AddTransient<ITemplateReader, TemplateReader>();
            services.AddTransient<ITemplateTagCollection, TemplateTagCollection>();
        }

    }
}
