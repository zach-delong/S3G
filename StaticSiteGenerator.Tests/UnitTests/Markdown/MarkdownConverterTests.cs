using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Markdig;
using Markdig.Syntax;
using NSubstitute;
using NSubstitute.Extensions;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Tests.AutoFixture;
using Xunit;
using static StaticSiteGenerator.Markdown.DocumentPropertyReader;

namespace StaticSiteGenerator.Tests.UnitTests.Markdown;

public class MarkdownConverterTests: MockingTestBase
{

    [Theory]
    [MemberData(nameof(TestCaseData))]
    public void Test(
	string inputString,
	DocumentProperties documentProperties,
	string expectedOutput)
    {
        Mocker.SetupCustomPipelineFactory();
        Mocker.SetupDocumentPropertyReader(documentProperties);

        var converter = Mocker.Create<MarkdownConverter>();

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
    public static void SetupCustomPipelineFactory(this IFixture mocker)
    {
        var mock = Substitute.ForPartsOf<CustomMarkdownPipelineFactory>((CustomExtension)null);

	mock
	    .Configure()
	    .Get()
            .Returns((new MarkdownPipelineBuilder()).Build());

        mocker.Inject(mock);
    }

    public static DocumentPropertyReader SetupDocumentPropertyReader(this IFixture mocker, DocumentProperties result)
    {
        var mock = Substitute.ForPartsOf<DocumentPropertyReader>((OnPropertiesFound)null);

        mock.Configure()
	    .GetDocumentProperties(Arg.Any<MarkdownDocument>())
            .Returns(result);

        mocker.Inject(mock);

        return mock;
    }
}

