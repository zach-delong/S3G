using Microsoft.Extensions.Logging;
using Moq;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles;

public class LoggerMockFactory
{
    public Mock<ILogger<T>> Get<T>() where T : class => new Mock<ILogger<T>>();
}
