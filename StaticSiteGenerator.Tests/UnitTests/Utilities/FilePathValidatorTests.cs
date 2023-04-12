using FluentAssertions;
using StaticSiteGenerator.Utilities;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Utilities;

public class FilePathValidatorTests
{
    [Theory]
    [InlineData("http://stuff.com", false)]
    [InlineData("https://stuff.com", false)]
    [InlineData("ftp://stuff", false)]
    [InlineData("", false)]
    [InlineData("/this/is/a/path", true)]
    [InlineData("/this/is/a/path.md", true)]
    [InlineData("this/is/a/path.md", true)]
    [InlineData(@"C:\this\is\a\path.md", true)]
    public void Test(string input, bool expectedResult)
    {
        var sut = new FilePathValidator();

        var result = sut.IsFilePath(input);
	
	if(expectedResult)
            result.Should().BeTrue();
	else
            result.Should().BeFalse();
    }
}
