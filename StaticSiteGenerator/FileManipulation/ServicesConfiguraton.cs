using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.FileManipulation.FileListing;
using StaticSiteGenerator.FileManipulation.FileWriting;
using StaticSiteGenerator.Utilities.DependencyInjection;

namespace StaticSiteGenerator.FileManipulation
{
    public static class ServicesConfiguration
    {
        public static void AddFileManipulationServices(this IServiceCollection services)
        {
            services.AddTransient<IFileWriter, SystemFileWriter>();
            services.Decorate<IFileWriter, OverwritingFileWriter>();

            services.AddTransient<IDirectoryEnumerator, DeferredExecutionDirectoryEnumerator>();
        }
    }
}
