using System;
using System.Collections.Generic;
using StaticSiteGenerator.Utilities.Date;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Utilities.Date;

public class DateParserTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void DatesShouldParse(string input,
                                 bool expectedSuccess,
                                 DateTime expectedOutput)
    {
        var parser = new DateParser();

        DateTime output;
        var success = parser.TryParse(input, out output);

        Assert.Equal(expectedSuccess, success);
        Assert.Equal(expectedOutput, output);
    }

    public static IEnumerable<object[]> TestData = new List<object[]> {
            new object[] {"1/1/2020", true, new DateTime(2020, 1, 1, 0, 0, 0)},
            new object[] {"12/31/2020", true, new DateTime(2020, 12, 31, 0, 0, 0)},
            new object[] {"random_junk", false, null},
            new object[] {"", false, null},
            new object[] {null, false, null},
        };
}
