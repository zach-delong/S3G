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

        private IList<TemplateTag> TemplateParts;

        public TemplateReader(
            FileIterator fileIterator,
            FileReader fileReader
        ){
            FileIterator = fileIterator;
            FileReader = fileReader;

            TemplateParts = new List<TemplateTag>();
        }

        public IList<TemplateTag> ReadTemplate(string templatePath)
        {
            foreach(var filePath in FileIterator.GetFilesInDirectory(templatePath))
            {
                var template = ReadTemplateFile(filePath);
                TemplateParts.Add(template);
            }

            return TemplateParts;
        }

        public TemplateTag ReadTemplateFile(string filePath)
        {
            var name = Path
                .GetFileName(filePath)
                .Replace(".html", "");

            var template = GetProperTagWriterFor(name);

            template.Template = FileReader.ReadFile(filePath).ReadToEnd();

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

        public TemplateTag GetTemplateTagForType(TagType type)
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
