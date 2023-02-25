using System.Text;

namespace StaticSiteGenerator.Markdown;

public class DocumentProperties
{
    public string Title { get; set; } = string.Empty;
    public bool Published { get; set; } = true;

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"Document Properties:");
        builder.AppendLine($"{nameof(Title)}:\t{Title}");
        builder.AppendLine($"{nameof(Published)}:\t{Published}");

        return builder.ToString();
    }
}
