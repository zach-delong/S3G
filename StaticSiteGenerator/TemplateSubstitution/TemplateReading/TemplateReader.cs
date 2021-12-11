using System;
using System.IO;
using System.Collections.Generic;

using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Files.FileListing;
using StaticSiteGenerator.Files;

namespace StaticSiteGenerator.TemplateReading;

public class TemplateReader : ITemplateReader
{
    readonly IDirectoryEnumerator DirectoryEnumerator;
    readonly FileReader FileReader;

    public CliOptions Options { get; }

    public TemplateReader(
        IDirectoryEnumerator directoryEnumerator,
        FileReader fileReader,
        CliOptions options
    )
    {
        DirectoryEnumerator = directoryEnumerator;
        FileReader = fileReader;
        Options = options;
    }

    public IEnumerable<TemplateTag> ReadTemplate()
    {
        foreach (var filePath in DirectoryEnumerator.GetFiles(Path.Combine(Options.TemplatePath, "tag_templates"), "*.html"))
        {
            yield return ReadTemplateFile(filePath);
        }
    }

    public TemplateTag ReadTemplateFile(string filePath)
    {
        var name = Path
            .GetFileName(filePath)
            .Replace(".html", "");

        var template = GetTagWriterFor(name);

        template.Template = FileReader.ReadFile(filePath);

        return template;
    }

    private TemplateTag GetTagWriterFor(string fileName)
    {
        try
        {
            var template = new TemplateTag();
            TagType type = GetTagTypeForString(fileName);

            template.Type = type;

            return template;
        }
        catch (ArgumentException ex)
        {
            string value = $"There was an exception when converting template file names into template types. {fileName} did not convert cleanly";
            Console.WriteLine(value);
            throw (new ArgumentException(value, ex));
        }
    }

    private TagType GetTagTypeForString(string input)
    {
        switch (input)
        {
            case "h1":
                return TagType.Header1;
            case "p":
                return TagType.Paragraph;
            default:
                throw new ArgumentException(message: $"{input} is not a valid type for {nameof(TagType)}");
        }
    }
}
