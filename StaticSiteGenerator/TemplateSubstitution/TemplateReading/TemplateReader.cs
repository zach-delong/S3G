using System;
using System.IO;
using System.Collections.Generic;

using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Files.FileListing;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.CLI;

namespace StaticSiteGenerator.TemplateReading;

public class TemplateReader : ITemplateReader
{
    readonly IDirectoryEnumerator DirectoryEnumerator;
    readonly FileReader FileReader;
    readonly TemplatePathOption templatePathOption;

    public TemplateReader(
        IDirectoryEnumerator directoryEnumerator,
        FileReader fileReader,
        TemplatePathOption templatePathOption)
    {
        DirectoryEnumerator = directoryEnumerator;
        FileReader = fileReader;
        this.templatePathOption = templatePathOption;
    }

    public IEnumerable<TemplateTag> ReadTemplate()
    {
        foreach (var filePath in DirectoryEnumerator.GetFiles(Path.Combine(templatePathOption.TemplatePath, "tag_templates"), "*.html"))
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
            case "h2":
                return TagType.Header2;
            case "h3":
                return TagType.Header3;
            case "h4":
                return TagType.Header4;
            case "h5":
                return TagType.Header5;
            case "h6":
                return TagType.Header6;
            case "p":
                return TagType.Paragraph;
            case "link":
                return TagType.Link;
            case "image":
                return TagType.Image;
            case "ol":
                return TagType.OrderedList;
            case "ul":
                return TagType.UnorderedList;
            case "li":
                return TagType.ListItem;
            case "i":
                return TagType.Italic;
            case "b":
                return TagType.Bold;
	    case "code_block":
                return TagType.CodeBlock;
            default:
                throw new ArgumentException(message: $"{input} is not a valid type for {nameof(TagType)}");
        }
    }
}
