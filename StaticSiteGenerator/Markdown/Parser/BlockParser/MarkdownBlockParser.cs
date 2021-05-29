using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Markdown.Parser.BlockParser
{

    public class MarkdownBlockParser: IMarkdownBlockParser
    {
        StrategyCollection<IBlockElementConverter> Converters;

        public MarkdownBlockParser(StrategyCollection<IBlockElementConverter> converters)
        {
            Converters = converters;
        }

        public IList<IBlockElement> Parse(IList<MarkdownBlock> inputBlocks)
        {
            var list = new List<IBlockElement>();
            foreach(var block in inputBlocks)
            {
                list.Add(Parse(block));
            }

            return list;
        }

        public IBlockElement Parse(MarkdownBlock block)
        {
            var converter = Converters.GetConverterForType(block.GetType());

            return converter.Convert(block);
        }
    }
}
