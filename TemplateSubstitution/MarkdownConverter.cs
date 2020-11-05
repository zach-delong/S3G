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
            foreach(MarkdownBlock block in markdownFile.Blocks)
            {
                var result = block.GetType();
                Console.WriteLine(result);
            }
        }

    }
}