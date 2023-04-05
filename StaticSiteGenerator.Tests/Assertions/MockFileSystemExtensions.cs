using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using StaticSiteGenerator.Tests.Assertions.FileSystem;

namespace StaticSiteGenerator.Tests.Assertions;

public static class MockFileSystemExtensions
{

    [CustomAssertion]
    public static MockFileSystemAssertions Must(this MockFileSystem input)
    {
        return new MockFileSystemAssertions(input);
    }
}
