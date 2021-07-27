using StaticSiteGenerator.Markdown.InlineElement;
using Markdig.Syntax.Inlines;

namespace StaticSiteGenerator.Markdown.InlineElementConverter
{
    [MarkdownConverterForAttribute(nameof(LiteralInline))]
    public class TextElementConverter: IInlineElementConverter
    {
        public IInlineElement Convert(IInline inline)
        {
            var textRun = (LiteralInline)inline;

            return new Text
            {
                Content = Markdig.Markdown.ToPlainText(textRun.Content.Text)
            };
        }
    }
}
