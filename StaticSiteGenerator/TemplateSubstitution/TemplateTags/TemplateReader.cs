using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using StaticSiteGenerator.FileManipulation;

using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public class TemplateReader : ITemplateReader
    {
        readonly FileIterator FileIterator;
        readonly FileReader FileReader;

        public CliOptions Options { get; }

        public TemplateReader(
            FileIterator fileIterator,
            FileReader fileReader,
            CliOptions options
        )
        {
            FileIterator = fileIterator;
            FileReader = fileReader;
            Options = options;
        }

        public IEnumerable<TemplateTag> ReadTemplate()
        {
            foreach (var filePath in FileIterator.GetFilesInDirectory($"templates/{Options.TemplateName}"))
            {
                yield return ReadTemplateFile(filePath);
            }
        }

        public TemplateTag ReadTemplateFile(string filePath)
        {
            var name = Path
                .GetFileName(filePath)
                .Replace(".html", "");

            var template = GetProperTagWriterFor(name);

            template.Template = FileReader.ReadFile(filePath);

            return template;
        }

        private TemplateTag GetProperTagWriterFor(string fileName)
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
}
