using StaticSiteGenerator.IntegrationTests.Utilities.FluentAssertionExtensions;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests.ThirdParties.Markdown;

public class MarkDigParserTests
{
    [Fact]
    public void BasicConversion()
    {
	var exampleInput = "# Hello, world!";

        var document = Markdig.Markdown.ToHtml(exampleInput);

        document.Must().Contain("<h1>Hello, world!</h1");
    }

}
