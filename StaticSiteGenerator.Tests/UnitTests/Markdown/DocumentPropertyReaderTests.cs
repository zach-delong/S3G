using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Markdig;
using Markdig.Syntax;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Tests.AutoFixture;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.Markdown;

public class DocumentPropertyReaderTests: MockingTestBase
{
    [Theory]
    [MemberData(nameof(TestCaseData))]
    public void Test(MarkdownDocument document, DocumentProperties expectedDocumentProperties)
    {
        var reader = Mocker.Create<DocumentPropertyReader>();

        var result = reader.GetDocumentProperties(document);

        result.Should().BeEquivalentTo(expectedDocumentProperties);
    }

    public static IEnumerable<object[]> TestCaseData
    {
	get
	{
            var pipelineBuilder = new MarkdownPipelineBuilder();
            pipelineBuilder.UseYamlFrontMatter();

            var pipeline = pipelineBuilder.Build();

            yield return new object[]
            {
		new MarkdownDocument(),
		null
            };

            yield return new object[]
            {
		Markdig.Markdown.Parse("This is some markdown", pipeline),
		null
            };

            var markdownWithFrontmatter = @"---
published: false
title: test title
---";

            yield return new object[]
            {
		Markdig.Markdown.Parse(markdownWithFrontmatter, pipeline),
		new DocumentProperties { Title = "test title", Published = false}
            };

	    var markdownWithPublishedButNoTitle = @"---
published: false
---";
            yield return new object[] {
		Markdig.Markdown.Parse(markdownWithPublishedButNoTitle, pipeline),
		new DocumentProperties { Published = false }
	    };

        }
    }
}
