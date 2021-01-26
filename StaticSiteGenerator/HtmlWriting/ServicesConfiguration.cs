using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Utilities.DependencyInjection;

namespace StaticSiteGenerator.HtmlWriting
{
    public static class ServicesConfiguration
    {
        public static void AddHtmlWriting(this IServiceCollection services)
        {
            services.AddTransient<IHtmlFileWriter, FileSystemHtmlWriter>();
            services.Decorate<IHtmlFileWriter, MarkdownHtmlFileWriter>();
        }
    }
}
