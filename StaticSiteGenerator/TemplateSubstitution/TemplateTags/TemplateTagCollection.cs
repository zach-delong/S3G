using System;
using System.Collections.Generic;

namespace StaticSiteGenerator.TemplateSubstitution.TemplateTags
{
    public class TemplateTagCollection : ITemplateTagCollection
    {
        public ITemplateReader TemplateReader { get; }

        private IDictionary<TagType, TemplateTag> Tags;

        public TemplateTagCollection(ITemplateReader templateReader)
        {
            TemplateReader = templateReader;
            Tags = new Dictionary<TagType, TemplateTag>();

            foreach(var template in templateReader.ReadTemplate())
            {
                Add(template);
            }
        }

        private void Add(TemplateTag tag)
        {
            try { Tags.Add(tag.Type, tag); }
            catch(Exception e) when (e is ArgumentException
                                   || e is ArgumentNullException
                                   || e is NotSupportedException)
            {
                throw new ArgumentException($"There was a problem when inserting tag for {tag.Type}");
            }

        }

        public TemplateTag GetTagForType(TagType type)
        {
            try
            {
                return Tags[type];
            }
            catch(Exception e) when (e is ArgumentNullException
                                     || e is KeyNotFoundException)
            {
                throw new ArgumentException($"Could not find template for{type}");
            }
        }
    }
}
