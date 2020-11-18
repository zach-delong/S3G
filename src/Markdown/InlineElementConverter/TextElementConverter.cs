using StaticSiteGenerator.Markdown.InlineElement;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;

//TODO: move the attribute out of the block space so I can reuse it for elements
using StaticSiteGenerator.Markdown.BlockElementConverter;

namespace StaticSiteGenerator.Markdown.InlineElementConverter
{
    [MarkdownConverterForAttribute(nameof(TextRunInline))]
    public class TextElementConverter: IInlineElementConverter
    {
        public IInlineElement Convert(MarkdownInline inline)
        {
            var textRun = (TextRunInline)inline;

            return new Text{
                Content = textRun.Text
            };
        }
    }
}
