using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.FileManipulation.FileWriting;

namespace StaticSiteGenerator.FileManipulation
{
    public static class ServicesConfiguration
    {
        public static void AddFileManipulationServices(this IServiceCollection services)
        {
            services.AddTransient<IFileWriter, SystemFileWriter>();
        }
    }
}
