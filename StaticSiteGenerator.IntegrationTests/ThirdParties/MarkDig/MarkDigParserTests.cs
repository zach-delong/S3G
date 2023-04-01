using StaticSiteGenerator.IntegrationTests.Utilities.FluentAssertionExtensions;

namespace StaticSiteGenerator.IntegrationTests.ThirdParties.Markdown;

public class MarkDigParserTests
{
    public void BasicConversion()
    {
	var exampleInput = "#Hello, world!";

        var document = Markdig.Markdown.ToHtml(exampleInput);

        document.Must().Contain("<h1>Hello, world!</h1");
    }

}
