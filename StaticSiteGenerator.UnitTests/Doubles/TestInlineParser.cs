using System.Collections.Generic;
using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Markdown.InlineElement;
using Markdig.Syntax.Inlines;

namespace StaticSiteGenerator.UnitTests.Markdown.Doubles
{
    public class TestInlineParser: IMarkdownInlineParser
    {
        public bool ParseCalled = false;

        public IList<IInlineElement> Parse(ContainerInline inlines){
            ParseCalled = true;
            return null;
        }
    }
}
