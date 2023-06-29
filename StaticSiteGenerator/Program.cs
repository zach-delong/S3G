using System;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator;

public sealed class Program
{
    static void Main(string[] args)
    {
        CommandLine.Parser.Default.ParseArguments<CliOptions>(args)
            .WithParsed((CliOptions o) =>
            {
                var serviceProvider = BuildDependencies(o);

                serviceProvider.GetService<Generator>().Start();
            });
    }

    public static IServiceProvider BuildDependencies(CliOptions options)
    {
        var service = new ServiceCollection();
        service.AddCustomServices();

        service.AddSingleton(options);

        return service.BuildServiceProvider();
    }

}
