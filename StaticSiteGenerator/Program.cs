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
                ServiceCollectionFactory 
		    .Get(o)
		    .GetService<Generator>()
		    .Start();
            });
    }
}
