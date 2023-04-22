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

namespace StaticSiteGenerator.Tests.UnitTests.Extensions;

public class OrderedListExtensionsTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void TestReplace(OrderedList<IMarkdownObjectRenderer> targetList,
                            Type targetType,
                            IMarkdownObjectRenderer replaceWith,
                            OrderedList<IMarkdownObjectRenderer> expected)
    {
        targetList.ReplaceOrAdd(targetType, replaceWith);

        targetList.Should().BeEquivalentTo(expected);

    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            var codeBlockRenderer = new CodeBlockRenderer();
            var literalRenderer = new LiteralInlineRenderer();

            yield return new object[]
            {
                new OrderedList<IMarkdownObjectRenderer>(),
                typeof(HtmlObjectRenderer<LiteralInline>),
                null,
                new OrderedList<IMarkdownObjectRenderer> {
		    null
		}
            };

            yield return new object[]
            {
                new OrderedList<IMarkdownObjectRenderer>
                {
                    literalRenderer
                },
                typeof(HtmlObjectRenderer<LiteralInline>),
                null,
                new OrderedList<IMarkdownObjectRenderer> {
		    null
		}
            };

            yield return new object[]
            {
                new OrderedList<IMarkdownObjectRenderer>
                {
		    codeBlockRenderer,
		    literalRenderer
                },
                typeof(HtmlObjectRenderer<LiteralInline>),
                null,
                new OrderedList<IMarkdownObjectRenderer> {
		    codeBlockRenderer,
		    null
		}
            };

        }
    }
}
