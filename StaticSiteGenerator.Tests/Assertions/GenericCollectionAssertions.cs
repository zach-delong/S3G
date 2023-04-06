using System.Collections.Generic;
using FluentAssertions;

namespace StaticSiteGenerator.Tests.Assertions;

public class GenericCollectionAssertions<T>
{
    private readonly IEnumerable<T> Haystack;

    public GenericCollectionAssertions(IEnumerable<T> haystack)
    {
	Haystack = haystack;
    }

    [CustomAssertion]
    public void Contain(T needle, string because = "", params object[] objects)
    {
	Haystack.Should()
		.Contain(needle, because, objects);
    }

    [CustomAssertion]
    public void HaveCount(int expectedCount, string because = "", params object[] objects)
    {
        Haystack.Should()
            .HaveCount(expectedCount, because, objects);
    }
}
