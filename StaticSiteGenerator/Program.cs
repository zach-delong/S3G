using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CommandLine.Parser.Default.ParseArguments<CliOptions>(args)
                    .WithParsed(RunProgram)
                    .WithNotParsed(OptionsErrors);
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

            serviceProvider.GetService<StaticSiteGenerator>().Start();
        }

        public static IServiceProvider BuildDependencies(CliOptions options)
        {
            var service = new ServiceCollection();
            service.AddCustomServices();

            service.AddSingleton(options);

            return service.BuildServiceProvider();
        }

        static void OptionsErrors(IEnumerable<Error> errors)
        {
            Console.WriteLine(errors);
            throw new Exception("An error occurred while gathering options");
        }

    }
}
