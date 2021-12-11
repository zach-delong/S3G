using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.SiteTemplating.SiteTemplateReading;

namespace StaticSiteGenerator.SiteTemplating;

public static class ServicesConfiguration
{
    public static void AddSiteTemplateServices(this IServiceCollection services)
    {
        services.AddTransient<ISiteTemplateReader, SiteTemplateLocalFileReader>();
        services.AddTransient<ISiteTemplateFiller, SiteTemplateFiller>();
    }
}
