using System.Collections.Generic;
using StaticSiteGenerator.Markdown.InlineElement;

namespace StaticSiteGenerator.Markdown.BlockElement
{
    public class Header: IBlockElement
    {
        public IList<IInlineElement> Inlines { get; set; }

        public byte Level { get; set; }
        public string Text { get; set; }

        public string Content => Text;
    }
}
