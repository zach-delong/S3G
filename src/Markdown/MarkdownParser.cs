using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.MarkdownElementConverter;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.Markdown
{

    [TransientService]
    public class MarkdownParser: IMarkdownParser
    {
        IList<IMarkdownElementConverter> Converters;

        public MarkdownParser(
            MarkdownHeaderElementConverter headerConverter,
            MarkdownParagraphElementConverter paragraphConverter
        )
        {

            Converters = new List<IMarkdownElementConverter> {
                headerConverter,
                paragraphConverter
            };

        }

        public MarkdownDocument ParseMarkdownString(StreamReader markdownFileContents)
        {
            var markdownString = markdownFileContents.ReadToEnd();
            var parsedMarkdownDocument = ParseMarkdownString(markdownString);

            return parsedMarkdownDocument;
        }

        public MarkdownDocument ParseMarkdownString(string markdownFileContents)
        {
            var parsedMarkdownDocument= new MarkdownDocument();

            parsedMarkdownDocument.Parse(markdownFileContents);

            return parsedMarkdownDocument;
        }

        public IList<IBlockElement> Parse(string markdownFile)
        {
            var parsedMarkdownDocument = new MarkdownDocument();

            parsedMarkdownDocument.Parse(markdownFile);

            return Parse(parsedMarkdownDocument.Blocks);
        }

        private IList<IBlockElement> Parse(IList<MarkdownBlock> inputBlocks)
        {
            var list = new List<IBlockElement>();
            foreach(var block in inputBlocks)
            {
                list.Add(Parse(block));
            }

            return list;
        }

        private IBlockElement Parse(MarkdownBlock block)
        {
            var converter = GetElementConverterFor(block.GetType());

            return converter.Convert(block);
        }

        private IMarkdownElementConverter GetElementConverterFor(Type type)
        {
            foreach(var converter in Converters)
            {
                if(ConverterHasMatchingAttributeType(converter, type)){
                    return converter;
                }
            }

            // TODO: should probably thow a custom exception type
            throw new Exception($"Converter for type {type.Name} not found");
        }

        private bool ConverterHasMatchingAttributeType(IMarkdownElementConverter converter, Type type)
        {
            var converterType = converter.GetType();

            var attribute = (MarkdownConverterForAttribute) Attribute.GetCustomAttribute(converterType, typeof(MarkdownConverterForAttribute));

            return attribute?.TypeName == type.Name;
        }
    }
}
