using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.TemplateReading;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public static class ServicesConfiguration
    {
        public static void AddTemplateManagement(this IServiceCollection services)
        {
            services.AddTransient<ITemplateReader, TemplateReader>();
            services.AddTransient<ITemplateTagCollection, TemplateTagCollection>();

            services.AddTransient<ITemplateFiller, TemplateFiller>();
        }

    }
}
