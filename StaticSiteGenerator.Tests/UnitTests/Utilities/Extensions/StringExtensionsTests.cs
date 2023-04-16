using System;
using System.Collections.Generic;
using StaticSiteGenerator.Utilities.Extensions;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.Utilities.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(string target,
                     IDictionary<string, string> replacements,
                     string expectedResult)
    {
        var result = target.Replace(replacements);

        Assert.Equal(expectedResult, result);

        if(target != expectedResult)
        {
            Assert.NotEqual(target, result);
        }

    }


    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[]
            {
                "input",
                new Dictionary<string, string>
                {
                    {"input", "output"}
                },
                "output"
            };

            yield return new object[]
            {
                "",
                new Dictionary<string, string>
                {
                    {"foo", "bar"}
                },
                ""
            };

            yield return new object[]
            {
                "Test Two",
                new Dictionary<string, string>
                {
                    {"Test", "a"},
                    {"Two", "b"}
                },
                "a b"
            };
        }
    }

    [Theory]
    [MemberData(nameof(ExceptionTestData))]
    public void TestExceptions(string target,
                     IDictionary<string, string> replacements)
    {
        Assert.Throws<ArgumentException>(() => target.Replace(replacements));
    }

    public static IEnumerable<object[]> ExceptionTestData
    {
        get
        {
            yield return new object[]
            {
                "",
                new Dictionary<string, string>
                {
                    {"", ""}
                }
            };

            yield return new object[]
            {
                "",
                new Dictionary<string, string>
                {
                    {"", null}
                }
            };
        }
    }
}
