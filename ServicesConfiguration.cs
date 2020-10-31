using StaticSiteGenerator;
using Microsoft.Extensions.DependencyInjection;

public static class ServicesConfiguration
{
	public static void AddCustomServices(this IServiceCollection services)
	{
      services.AddSingleton<FileIterator, FileIterator>();
      services.AddTransient<StaticSiteGenerator.StaticSiteGenerator, StaticSiteGenerator.StaticSiteGenerator>();
      services.AddTransient<MarkdownFileReader, MarkdownFileReader>();
	}
}
