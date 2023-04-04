using FluentAssertions;

namespace StaticSiteGenerator.IntegrationTests.Utilities.FluentAssertionExtensions;

public class StringAssertions
{
    private readonly string input;

    public StringAssertions(string input)
    {
        this.input = input;
    }

    public void Contain(string substring, string reason="", params object[] parameters)
    {
        input.Should().Contain(substring, reason, parameters);
    }
}
