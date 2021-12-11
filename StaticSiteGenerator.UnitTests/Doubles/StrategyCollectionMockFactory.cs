using System;
using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.Utilities.StrategyPattern;
using StaticSiteGenerator.Utilities.StrategyPattern.Exceptions;

namespace Test.Markdown.Parser;

public class StrategyCollectionMockFactory
{
    public Mock<StrategyCollection<T>> Get<T>(IDictionary<string, T> strategyMappings)
    {
        var mock = new Mock<StrategyCollection<T>>(new List<T>());

        mock.Setup(c => c.GetStrategyForType(It.IsAny<Type>()))
            .Returns<Type>((p) =>
            {
                if (strategyMappings.TryGetValue(p.Name, out T value))
                {
                    return value;
                }

                throw new StrategyNotFoundException();
            }
          );

        return mock;
    }
}
