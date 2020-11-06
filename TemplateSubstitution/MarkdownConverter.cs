using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public class MarkdownConverter
    {
        public void Convert(MarkdownDocument markdownFile)
        {
            Convert(markdownFile.Blocks);
        }

        public void Convert(IList<MarkdownBlock> blockList)
        {
            foreach(var block in blockList){
                Convert(block);
            }
        }

        public void Convert(MarkdownBlock block){
            switch(block){
                case HeaderBlock b:
                    Convert(b);
                    break;
            }
        }

        public void Convert(HeaderBlock block){
            Console.WriteLine($"<h{block.HeaderLevel}>{block.ToString()}</h{block.HeaderLevel}>");
        }
    }
}
