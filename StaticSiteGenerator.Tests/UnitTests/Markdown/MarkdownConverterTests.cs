using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using Markdig;
using Markdig.Syntax;
using Moq;
using Moq.AutoMock;
using StaticSiteGenerator.Markdown;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown;

public class MarkdownConverterTests
{
    AutoMocker mocker = new AutoMocker();

    [Theory]
    [MemberData(nameof(TestCaseData))]
    public void Test(
	string inputString,
	DocumentProperties documentProperties,
	string expectedOutput)
    {
        mocker.SetupCustomPipelineFactory();
        mocker.SetupDocumentPropertyReader(documentProperties);
        var converter = mocker.CreateInstance<MarkdownConverter>();

	var result = converter.ConvertToHtml(inputString);

	using (new AssertionScope())
	{
	    result.Contents.Trim().Should().BeEquivalentTo(expectedOutput);
	    result.Properties.Should().Be(documentProperties);
	}
    }

    public static IEnumerable<object[]> TestCaseData
    {
        get
        {
            yield return new object[]
            {
		"Hello, world",
		null,
		"<p>Hello, world</p>"
            };

            yield return new object[]
            {
		"Hello, world",
		new DocumentProperties(),
		"<p>Hello, world</p>"
            };
        }
    }
}

public static class AutoMockerExtensions
{
    public static Mock<CustomMarkdownPipelineFactory> SetupCustomPipelineFactory(this AutoMocker mocker)
    {
        var mock = mocker.GetMock<CustomMarkdownPipelineFactory>();

        mock.Setup(m => m.Get())
            .Returns(new MarkdownPipelineBuilder().Build());

        return mock;
    }

    public static Mock<DocumentPropertyReader> SetupDocumentPropertyReader(this AutoMocker mocker, DocumentProperties result)
    {
        var mock = mocker.GetMock<DocumentPropertyReader>();

        mock.Setup(m => m.GetDocumentProperties(It.IsAny<MarkdownDocument>()))
        .Returns(result);

        return mock;
    }
}

