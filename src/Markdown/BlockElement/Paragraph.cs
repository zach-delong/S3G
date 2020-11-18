using System.Collections.Generic;

namespace StaticSiteGenerator.Markdown.BlockElement
{
    public class Paragraph: IBlockElement
    {
        public string Text { get; set; }

        public string Content => Text;
    }
}
