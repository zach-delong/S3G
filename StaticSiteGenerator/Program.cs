using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.CLI;

namespace StaticSiteGenerator;

public sealed class Program
{
    static void Main(string[] args)
    {
        var o = Parser.Default.ParseArguments<CliOptions>(args);

        if (o?.Value == null)
        {
	    // We were not able to parse some or all of the arguments...
            return;
        }

        ServiceCollectionFactory
	    .Get(o.Value)
	    .GetService<Generator>()
	    .Start();
    }
}
