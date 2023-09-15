using AutoFixture;
using FluentAssertions;
using StaticSiteGenerator.Tests.AutoFixture;
using StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;
using StaticSiteGenerator.Utilities;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.Utilities;

public class LinkProcessorTests: MockingTestBase
{
    [Theory]
    [InlineData("", "")]
    [InlineData("testing/stuff.md", "testing/stuff.html")]
    [InlineData("/testing/stuff.md", "/testing/stuff.html")]
    [InlineData("https://testing/stuff.md", "https://testing/stuff.md")]
    [InlineData("https://testing/stuff.html", "https://testing/stuff.html")]
    public void foo(string inputUrl, string expectedResult)
    {
        Mocker.MockFileSystem(new string[0]);
        Mocker.Inject(new FilePathValidator());

        ILinkProcessor sut = Mocker.Create<LinkProcessor>();

        var result = sut.Process(inputUrl);

        result.Should().Be(expectedResult);
    }
}
