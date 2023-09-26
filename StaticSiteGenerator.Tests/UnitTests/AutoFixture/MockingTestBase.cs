using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace StaticSiteGenerator.Tests.AutoFixture;

///<summary>
/// This is a base class that sets up an AutoFixture that can be used to
/// construct systems using NSubstitute mocks as dependencies.
///</summary>
public class MockingTestBase
{
    private IFixture fixture = new Fixture()
	.Customize(new AutoNSubstituteCustomization());

    public IFixture Mocker { get => fixture; set => fixture = value; }
}
