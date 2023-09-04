using NSubstitute;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles;

public static class CliOptionsFactory
{
    public static CliOptions Get(string templatePath = "templates/") {
        var options = Substitute.For<CliOptions>();

	options.TemplatePath
	    .Returns(templatePath);

        return options;
    }
}
