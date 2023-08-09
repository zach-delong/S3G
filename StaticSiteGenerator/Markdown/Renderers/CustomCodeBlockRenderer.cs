using System.Linq;
using System.Text.Json;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Markdown.Renderers;

public class CustomCodeBlockRenderer: CustomRendererBase<CodeBlock>
{
    private readonly ITemplateTagCollection tagCollection;

    public CustomCodeBlockRenderer(ITemplateTagCollection tagCollection)
    {
        this.tagCollection = tagCollection;
    }

    protected override void Write(HtmlRenderer renderer, CodeBlock obj)
    {
        var tag = tagCollection.GetTagForType(TagType.CodeBlock);
        var foo = tag.Template.Split("{{}}");

        var attributes = obj.TryGetAttributes();

        var languageClass = attributes?.Classes.FirstOrDefault(s => s.Contains("language-"));

        if (languageClass != null)
        {
	    foo[0] = foo[0].Replace("<code>", $@"<code class=""{languageClass}"">");
        }

        renderer.Write(foo[0]);

        renderer.WriteLeafRawLines(obj, true, true, true);

        renderer.Write(foo[1]);
    }
}
