using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using StaticSiteGenerator.FileManipulation;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [SingletonService]
    public class TemplateReader
    {
        readonly FileIterator FileIterator;
        readonly FileReader FileReader;

        private IList<TagWriter> TemplateParts;

        public TemplateReader(
            FileIterator fileIterator,
            FileReader fileReader
        )
        {
            FileIterator = fileIterator;
            FileReader = fileReader;

            TemplateParts = new List<TagWriter>();
        }

        public IList<TagWriter> ReadTemplate(string templatePath)
        {
            foreach(var filePath in FileIterator.GetFilesInDirectory(templatePath))
            {
                var name = Path
                    .GetFileName(filePath)
                    .Replace(".html", "");

                var template = GetPropertTagWriterFor(name);

                template.Template = FileReader.ReadFile(filePath).ReadToEnd();

                TemplateParts.Add(template);
            }

            return TemplateParts;
        }

        private TagWriter GetPropertTagWriterFor(string fileName)
        {
            try
            {
                var template = new TagWriter();
                TagType type = GetTagTypeForString(fileName);

                template.Type = type;

                return template;
            }
            catch(ArgumentException ex){
                Console.WriteLine($"There was an exception when converting template file names into template types. {fileName} did not convert cleanly");
                throw(ex);
            }
        }

        private TagType GetTagTypeForString(string input)
        {
            switch(input)
            {
                case "h1":
                    return TagType.Header1;
                case "p":
                    return TagType.Paragraph;
                default:
                    throw new ArgumentException(message: $"{input} is not a valid type for {nameof(TagType)}");
            }
        }

        public TagWriter GetTemplateTagForType(TagType type)
        {
            try
            {
                return TemplateParts
                    .Single(p => p.Type == type);
            }
            catch(Exception ex)
            {
                throw new ArgumentException($"Could not find a template tag for the type {type}", ex);
            }
        }
    }
}
