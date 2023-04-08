using System;
using System.Collections.Generic;
using FluentAssertions;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Utilities.Extensions;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Extensions;

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

            foo.Should().BeEquivalentTo(replaceWith);
        }
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[]
            {
                new OrderedList<IMarkdownObjectRenderer>(),
                typeof(HtmlObjectRenderer<LiteralInline>),
                null,
                null
            };

            yield return new object[]
            {
                new OrderedList<IMarkdownObjectRenderer>
                {
                    new LiteralInlineRenderer()
                },
                typeof(HtmlObjectRenderer<LiteralInline>),
                null,
                0
            };

        }
    }
}
