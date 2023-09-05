using System;
using Moq;
using Moq.AutoMock;
using NSubstitute;
using StaticSiteGenerator.Utilities;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles;

public static class LinkProcessorFactory
{
    public static void MockLinkProcessor(this AutoMocker mocker)
    {
        mocker
            .GetMock<ILinkProcessor>()
            .Setup(m => m.Process(It.IsAny<string>()))
	    .Returns<string>(i => i);
    }

    public static ILinkProcessor Get()
    {
        var mock = Substitute.For<ILinkProcessor>();

        mock
	    .Process(Arg.Any<string>())
	    .Returns(args => (string)args[0]);

        return mock;
    }
}
