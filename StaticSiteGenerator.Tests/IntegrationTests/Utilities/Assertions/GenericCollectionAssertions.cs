using System.Collections.Generic;
using FluentAssertions;

namespace StaticSiteGenerator.IntegrationTests.Utilities.Assertions;

public class GenericCollectionAssertions<T>
{
    private readonly IEnumerable<T> Haystack;

    public GenericCollectionAssertions(IEnumerable<T> haystack)
    {
        Haystack = haystack;
    }

    [CustomAssertion]
    public void Contain(T needle, string because="", params object[] objects)
    {
        Haystack.Should()
                .Contain(needle, because, objects);
    }
}
