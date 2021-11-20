
using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.UnitTests.Doubles
{
    public class StrategyExecutorMockFactory
    {
        public IMock<IStrategyExecutor<R,I>> Get<R, I>(IEnumerable<R> result)
        {
            var mock = new Mock<IStrategyExecutor<R, I>>();

            mock.Setup(m => m.Process(It.IsAny<IEnumerable<I>>()))
                .Returns(result);

            return mock;
        }
    }
}
