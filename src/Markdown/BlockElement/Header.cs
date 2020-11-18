using System.Collections.Generic;

namespace StaticSiteGenerator.Markdown.BlockElement
{
    public class Header: IBlockElement
    {
        public byte Level { get; set; }
        public string Text { get; set; }

        public string Content => Text;
    }
}
