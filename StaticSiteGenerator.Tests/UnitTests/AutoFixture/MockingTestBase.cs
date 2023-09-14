using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace StaticSiteGenerator.Tests.AutoFixture;

public class MockingTestBase
{
    private IFixture mocker = new Fixture()
	.Customize(new AutoNSubstituteCustomization());

    public IFixture Mocker { get => mocker; set => mocker = value; }
}
