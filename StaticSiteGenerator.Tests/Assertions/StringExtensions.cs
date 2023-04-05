using FluentAssertions;

namespace StaticSiteGenerator.Tests.Assertions;

public static class FileSystemExtensions
{

    [CustomAssertion]
    public static StringAssertions Must(this string input)
    {
        return new StringAssertions(input);
    }
}
