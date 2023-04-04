using System.IO.Abstractions.TestingHelpers;

namespace StaticSiteGenerator.IntegrationTests.Utilities.Assertions.FileSystem;

public static class MockFileSystemExtensions
{
    public static MockFileSystemAssertions Should(this MockFileSystem system)
    {
	return new MockFileSystemAssertions(system);
    }
}
