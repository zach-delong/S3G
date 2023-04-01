using System.IO.Abstractions.TestingHelpers;

namespace StaticSiteGenerator.IntegrationTests.Utilities.FluentAssertionExtensions;

public static class FileSystemAssertions
{
    public static MockFileSystemAssertions Should(this MockFileSystem system)
    {
	return new MockFileSystemAssertions(system);
    }
}
