using FluentAssertions;

namespace StaticSiteGenerator.IntegrationTests.Utilities.Assertions;

public static class FileSystemExtensions
{

    [CustomAssertion]
    public static StringAssertions Must(this string input)
    {
        return new StringAssertions(input);
    }
}
