using AutoFixture;
using NSubstitute;
using StaticSiteGenerator.Utilities;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles;

public static class LinkProcessorFactory
{
    public static void MockLinkProcessor(this IFixture mocker)
    {
        mocker
            .Freeze<ILinkProcessor>()
            .Process(Arg.Any<string>())
	    .Returns(args => args[0].ToString());
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
