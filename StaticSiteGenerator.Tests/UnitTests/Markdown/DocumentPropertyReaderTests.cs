using System.Collections.Generic;
using FluentAssertions;
using Markdig;
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
