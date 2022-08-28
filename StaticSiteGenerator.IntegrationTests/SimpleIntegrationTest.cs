using Xunit;

namespace StaticSiteGenerator.IntegrationTests;

public abstract class SimpleIntegrationTest: IntegrationTestBase
{
    [Fact]
    public void Test()
    {
        Arrange();
        Act();
        Assert();
    }

    protected abstract void Assert();
    protected abstract void Act();
    protected abstract void Arrange();
}
