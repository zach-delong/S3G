using Moq.AutoMock;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using StaticSiteGenerator.Utilities;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Utilities;

public class LinkProcessorTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("testing/stuff.md", "testing/stuff.html")]
    [InlineData("/testing/stuff.md", "/testing/stuff.html")]
    [InlineData("https://testing/stuff.md", "https://testing/stuff.md")]
    [InlineData("https://testing/stuff.html", "https://testing/stuff.html")]
    public void foo(string inputUrl, string expectedResult)
    {
        var mocker = new AutoMocker();

        mocker.MockFileSystem(new string[0]);
        mocker.Use<FilePathValidator>(new FilePathValidator());

        ILinkProcessor sut = mocker.CreateInstance<LinkProcessor>();

        var result = sut.Process(inputUrl);

        Assert.Equal(expectedResult, result);
    }
}
