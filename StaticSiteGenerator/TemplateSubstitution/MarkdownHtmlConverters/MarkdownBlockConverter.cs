using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies;
using StaticSiteGenerator.Markdown.BlockElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using System.Linq;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters
{
    [TransientService]
    public class MarkdownBlockConverter: IHtmlConverter<IList<IBlockElement>>, IHtmlConverter<IBlockElement>
    {
        private readonly StrategyCollection<IBlockHtmlConverterStrategy> BlockConverters;

        public MarkdownBlockConverter(StrategyCollection<IBlockHtmlConverterStrategy> blockConverters)
        {
            BlockConverters = blockConverters;
        }

        public string Convert(IList<IBlockElement> blocks)
        {
            return string.Join(Environment.NewLine, blocks.Select(b => Convert(b)));
        }

        public string Convert(IBlockElement block)
        {
            var blockConverter = BlockConverters.GetConverterForType(block.GetType());

            return blockConverter.Convert(block);
        }
    }
}
