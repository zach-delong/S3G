using System.Collections.Generic;
using FluentAssertions;

namespace StaticSiteGenerator.Tests.Assertions;

public static class GenericCollectionExtensions
{
    [CustomAssertion]
    public static GenericCollectionAssertions<T> Must<T>(this IEnumerable<T> actualValue)
    {
	return new GenericCollectionAssertions<T>(actualValue);
    }
}
