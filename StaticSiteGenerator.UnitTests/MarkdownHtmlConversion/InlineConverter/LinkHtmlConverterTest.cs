using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;
using StaticSiteGenerator.MarkdownHtmlConversion.TagModelConverters;
using StaticSiteGenerator.TemplateSubstitution.TemplateFilling;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.UnitTests.Doubles;
using Xunit;

namespace StaticSiteGenerator.UnitTests.MarkdownHtmlConversion.InlineConverter;

public class LinkHtmlConverterTest
{
    TemplateCollectionMockFactory TagCollectionMockFactory => new TemplateCollectionMockFactory();

    [Theory, MemberData(nameof(TestData))]
    public void Test(IInlineElement linkElement, string expectedResult)
    {
        var templateTagcollection = TagCollectionMockFactory.Get(new List<TemplateTag>{
                new TemplateTag
                {
                    Type = TagType.Link,
                    Template = "<a href='{{url}}'>{{display_text}}</a>"
                }
            });
        var templateFiller = new TemplateFiller();

        var sut = new LinkConverter(
            new LinkInlineModelConverter(),
            templateFiller,
            templateTagcollection.Object
        );

        var result = sut.Execute(linkElement);

        Assert.Equal(expectedResult, result);
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[] { new LinkElement { Link = "foo", Text = "stuff" }, "<a href='foo'>stuff</a>" };
            yield return new object[] { new LinkElement { Link = "", Text = "" }, "<a href=''></a>" };
        }
    }
}
