using StaticSiteGenerator.Markdown.InlineElement;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;

namespace StaticSiteGenerator.Markdown.InlineElementConverter
{
    [MarkdownConverterForAttribute(nameof(TextRunInline))]
    public class TextElementConverter: IInlineElementConverter
    {
        public IInlineElement Convert(MarkdownInline inline)
        {
            var textRun = (TextRunInline)inline;

            return new Text {
                Content = textRun.Text
            };
        }
    }
}
