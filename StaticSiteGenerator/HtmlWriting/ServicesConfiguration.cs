using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.HtmlWriting
{
    public static class ServicesConfiguration
    {
        public static void AddHtmlWriting(this IServiceCollection services)
        {
            services.AddTransient<IFileSystemHtmlWriter, FileSystemHtmlWriter>();
            services.AddTransient<IHtmlFileWriter, MarkdownHtmlFileWriter>();
        }
    }
}
