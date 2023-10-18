using AutoFixture;
using StaticSiteGenerator.CLI;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles;

public static class CliOptionsFactory
{
    public static void SetupCliOptions(
	this IFixture fixture,
	CliOptionsBuilder builder)
    {
        var testOptions = builder.Build();

        fixture.Inject<MarkdownFilePathOption>(testOptions);
        fixture.Inject<TemplatePathOption>(testOptions);
        fixture.Inject<OutputLocationOption>(testOptions);
    }
}

public class CliOptionsBuilder
{
    private readonly CliOptions OptionsUnderConstruction;

    public CliOptionsBuilder()
    {
        OptionsUnderConstruction = new CliOptions
        {
	    PathToMarkdownFiles = "input/",
	    TemplatePath = "templates/",
	    OutputLocation = "output/",
        };
    }

    public CliOptions Build()
    {
        return OptionsUnderConstruction;
    }

    public CliOptionsBuilder WithPathToMarkdownFiles(string path)
    {
        OptionsUnderConstruction.PathToMarkdownFiles = path;

        return this;
    }

    public CliOptionsBuilder WithTemplatePath(string path)
    {
        OptionsUnderConstruction.TemplatePath = path;
        return this;
    }

    public CliOptionsBuilder WithOutputLocation(string path)
    {
        OptionsUnderConstruction.OutputLocation = path;
        return this;
    }

    public static CliOptionsBuilder Get()
    {
        return new CliOptionsBuilder();
    }
}
