using Moq;
using Moq.AutoMock;
using StaticSiteGenerator.Utilities;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles;

public static class UtilitiesAutoMockerExtensions
{
    public static void MockLinkProcessor(this AutoMocker mocker)
    {
        mocker
            .GetMock<ILinkProcessor>()
            .Setup(m => m.Process(It.IsAny<string>()))
	    .Returns<string>(i => i);
    }
}
