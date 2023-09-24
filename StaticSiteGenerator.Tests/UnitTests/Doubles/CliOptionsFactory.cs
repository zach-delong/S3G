using AutoFixture;
using NSubstitute;
using NSubstitute.Extensions;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles;

public static class CliOptionsFactory
{
    public static CliOptions Get(
	string templatePath = "templates/",
	string pathToMarkdownFiles = "input",
	string outputLocation = "output") {

	var mock = Substitute.ForPartsOf<CliOptions>();

        mock
            .Configure()
            .TemplatePath
            .Returns(templatePath);

        mock
            .Configure()
            .PathToMarkdownFiles
            .Returns(pathToMarkdownFiles);

        mock
            .Configure()
            .OutputLocation
            .Returns(outputLocation);

        return mock;
    }

    public static void SetupCliOptions(
	this IFixture fixture,
	string templatePath = null,
	string pathToMarkdownFiles = null,
	string outputLocation = null)
    {
        var mock = Get(
	    templatePath: templatePath,
	    pathToMarkdownFiles: pathToMarkdownFiles,
	    outputLocation: outputLocation
	);

        fixture.Inject(mock);
    }
}
