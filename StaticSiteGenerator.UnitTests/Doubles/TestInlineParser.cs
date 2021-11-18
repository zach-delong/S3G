using System.Collections.Generic;
using StaticSiteGenerator.Markdown.InlineElement;
using System.Collections;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Markdig.Syntax.Inlines;

namespace StaticSiteGenerator.UnitTests.Markdown.Doubles
{
    public class TestInlineParser: IStrategyExcecutor<IInline, IInlineElement>
    {
        public bool ParseCalled = false;

        public IEnumerable<IInlineElement> Process(IEnumerable inputs)
        {
            ParseCalled = true;
            return null;
        }
    }
}
