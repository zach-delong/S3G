using System.IO.Abstractions.TestingHelpers;

namespace StaticSiteGenerator.Tests.Assertions.FileSystem;

public static class MockFileSystemExtensions
{
    public static MockFileSystemAssertions Should(this MockFileSystem system)
    {
	return new MockFileSystemAssertions(system);
    }
}
