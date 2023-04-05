using FluentAssertions;

namespace StaticSiteGenerator.Tests.Assertions;

public class StringAssertions
{
    private readonly string input;

    public StringAssertions(string input)
    {
        this.input = input;
    }

    [CustomAssertion]
    public void Contain(string substring, string reason="", params object[] parameters)
    {
        input.Should().Contain(substring, reason, parameters);
    }
}