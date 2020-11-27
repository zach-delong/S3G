using StaticSiteGenerator.Markdown;
using System.Collections.Generic;

using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Markdown.InlineElement;

namespace Test.Markdown.Doubles
{
    public class TestInlineParser: IMarkdownInlineParser
    {
        public bool ParseCalled = false;

        public IList<IInlineElement> Parse(IList<MarkdownInline> inlines){
            ParseCalled = true;
            return null;
        }
    }
}
