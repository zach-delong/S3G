using System.Collections.Generic;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Utilities;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Utilities;

public class HeaderLevelHelperTests
{
    [Theory, MemberData(nameof(TestData))]
    public void Test(int level, TagType expectedTagType)
    {
        var helper = new HeaderLevelHelper();

        var result = helper.GetHeaderTagTypeFor(level);

        Assert.Equal(expectedTagType, result);
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[] { 1, TagType.Header1 };
            yield return new object[] { 2, TagType.Header2 };
            yield return new object[] { 3, TagType.Header3 };
            yield return new object[] { 4, TagType.Header4 };
            yield return new object[] { 5, TagType.Header5 };
            yield return new object[] { 6, TagType.Header6 };
            yield return new object[] { 7, TagType.Header6 };
            yield return new object[] { 100, TagType.Header6 };
        }
    }
}
