using System.Collections.Generic;
using FluentAssertions;
using Markdig.Syntax;
using Moq.AutoMock;
using StaticSiteGenerator.Markdown;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown;

public class DocumentPropertyReaderTests
{
    AutoMocker mocker = new AutoMocker();
    [Theory]
    [MemberData(nameof(TestCaseData))]
    public void Test(MarkdownDocument document, DocumentProperties expectedDocumentProperties)
    {
        var reader = mocker.CreateInstance<DocumentPropertyReader>();

        var result = reader.GetDocumentProperties(document);

        result.Should().BeEquivalentTo(expectedDocumentProperties);
    }

    public static IEnumerable<object[]> TestCaseData
    {
	get
	{
            yield return new object[]
            {
		new MarkdownDocument(),
		null
            };
        }
    }
}
