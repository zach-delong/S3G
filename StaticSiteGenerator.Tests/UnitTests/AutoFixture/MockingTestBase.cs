using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace StaticSiteGenerator.Tests.AutoFixture;

public class MockingTestBase
{
    private IFixture fixture = new Fixture()
	.Customize(new AutoNSubstituteCustomization());

    public IFixture Mocker { get => fixture; set => fixture = value; }
}
