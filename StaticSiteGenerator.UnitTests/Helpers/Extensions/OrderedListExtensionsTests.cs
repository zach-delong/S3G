using System;
using System.Collections.Generic;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Renderers.Html.Inlines;
using StaticSiteGenerator.Utilities.Extensions;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Helpers.Extensions;

public class OrderedListExtensionsTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void TestReplace(OrderedList<IMarkdownObjectRenderer> targetList,
                            Type targetType,
                            IMarkdownObjectRenderer replaceWith,
                            int? expectedIndex)
    {
        targetList.ReplaceOrAdd(targetType, replaceWith);

        if(expectedIndex != null)
        {
            var foo = targetList[expectedIndex.Value];

            Assert.Equal(replaceWith, foo);
        }
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[]
            {
                new OrderedList<IMarkdownObjectRenderer>(),
                typeof(LiteralInlineRenderer),
                null,
                null
            };

            yield return new object[]
            {
                new OrderedList<IMarkdownObjectRenderer>
                {
                    new LiteralInlineRenderer()
                },
                typeof(LiteralInlineRenderer),
                null,
                0
            };

        }
    }
}
