namespace StaticSiteGenerator.IntegrationTests.Utilities.FluentAssertionExtensions;

public static class StringExtensions
{
    public static StringAssertions Must(this string input)
    {
        return new StringAssertions(input);
    }
}
