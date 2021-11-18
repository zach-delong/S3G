using System.Collections.Generic;
using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Markdown.InlineElement;
using Markdig.Syntax.Inlines;
using System.Collections;

namespace StaticSiteGenerator.UnitTests.Markdown.Doubles
{
    public class TestInlineParser: IMarkdownInlineParser
    {
        public bool ParseCalled = false;

        public IEnumerable<IInlineElement> Process(IEnumerable inputs)
        {
            ParseCalled = true;
            return null;
        }
    }
}
