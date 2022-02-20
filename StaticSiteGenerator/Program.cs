using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            CommandLine.Parser.Default.ParseArguments<CliOptions>(args)
                .WithParsed(RunProgram);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception was thrown");
            Console.Write(ex);
        }
    }

    static void RunProgram(CliOptions o)
    {
        var serviceProvider = BuildDependencies(o);

        serviceProvider.GetService<Generator>().Start();
    }

    public static IServiceProvider BuildDependencies(CliOptions options)
    {
        var service = new ServiceCollection();
        service.AddCustomServices();

        service.AddSingleton(options);

        return service.BuildServiceProvider();
    }

}
