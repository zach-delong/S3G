using System.Globalization;
using StaticSiteGenerator.HtmlWriting;

namespace StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;

public class TitlePropertyFiller : IHtmlFilePropertyFillerStrategy
{
    private string titleMergeFieldMarkup => $"{{{{{nameof(IHtmlFile.Title).ToCamelCase()}}}}}";

    public string Execute(IHtmlFile file)
    {
        string titleValueOrDefault = file.Title ?? string.Empty;
        var replacedContent = file.HtmlContent.Replace(titleMergeFieldMarkup, titleValueOrDefault);

        return replacedContent;
    }
}

public static class StringExtension
{
    public static string ToCamelCase(this string str)
    {
        var array = str.ToCharArray();
        array[0] = char.ToLower(array[0]);

        return new string(array);
    }
}
