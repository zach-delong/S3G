using StaticSiteGenerator.FileManipulation;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.TemplateSubstitution;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

public static class ServicesConfiguration
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddServicesWithAttributeOfType<TransientServiceAttribute>();
    }
}
