using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.BlockConverters;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public static class ServicesConfiguration
    {
        public static void AddHtmlConverters(this IServiceCollection services)
        {
            services.AddTransient<IHtmlConverter<IBlockElement>, HeaderConverter>();
            services.AddTransient<IHtmlConverter<IBlockElement>, ParagraphConverter>();
        }

    }
}
