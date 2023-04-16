using System.Collections.Generic;
using FluentAssertions;
using StaticSiteGenerator.TemplateSubstitution.TemplateFilling;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.TemplateSubstitution;

public class ITemplateFillerTests
{
    [Theory]
    [InlineData("{{}}", "content", "content")]
    [InlineData("", "stuff", "")]
    [InlineData("{{as}}", "foo", "{{as}}")]
    [InlineData("", "", "")]
    public void TestFill(string templateString, string content, string expected)
    {
        var tag = new TemplateTag
        {
            Template = templateString
        };

        var filler = new TemplateFiller();

        var result = filler.Fill(tag, content);

        result.Should().BeEquivalentTo(expected);
    }

    [Theory, MemberData(nameof(TestData))]
    public void TestDictionaryFill(TemplateTag tag,
                                   IDictionary<string, string> content,
                                   string expectedResult)
    {
        var sut = new TemplateFiller();

        var result = sut.Fill(tag, content);

        result.Should().BeEquivalentTo(expectedResult);
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[]
            {
                new TemplateTag
                {
                    Template = ""
                },
                new Dictionary<string, string>(),
                ""
            };

            yield return new object[]
            {
                new TemplateTag
                {
                    Template = "{{content}}"
                },
                new Dictionary<string, string>
                {
                    { "content", "THINGS" }
                },
                "THINGS"
            };

            yield return new object[]
            {
                new TemplateTag
                {
                    Template = "{{cOntEnt}}"
                },
                new Dictionary<string, string>
                {
                    { "content", "THINGS" }
                },
                "{{cOntEnt}}"
            };
        }
    }
}
