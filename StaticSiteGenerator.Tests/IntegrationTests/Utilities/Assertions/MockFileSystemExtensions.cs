using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using StaticSiteGenerator.IntegrationTests.Utilities.Assertions.FileSystem;

namespace StaticSiteGenerator.IntegrationTests.Utilities.Assertions;

public static class MockFileSystemExtensions
{

    [CustomAssertion]
    public static MockFileSystemAssertions Must(this MockFileSystem input)
    {
        return new MockFileSystemAssertions(input);
    }
}
