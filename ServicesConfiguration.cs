using StaticSiteGenerator.FileManipulation;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator;
using StaticSiteGenerator.Markdown;

public static class ServicesConfiguration
{
	public static void AddCustomServices(this IServiceCollection services)
	{
      services.AddSingleton<FileIterator, FileIterator>();
      services.AddTransient<StaticSiteGenerator.StaticSiteGenerator, StaticSiteGenerator.StaticSiteGenerator>();
      services.AddTransient<MarkdownFileReader, MarkdownFileReader>();

      services.AddTransient<MarkdownParser, MarkdownParser>();
	}
}
