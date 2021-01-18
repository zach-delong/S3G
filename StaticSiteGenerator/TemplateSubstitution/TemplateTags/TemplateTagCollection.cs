using System;
using System.Collections.Generic;
using StaticSiteGenerator.TemplateReading;

namespace StaticSiteGenerator.TemplateSubstitution.TemplateTags
{
    public class TemplateTagCollection : ITemplateTagCollection
    {
        private ITemplateReader TemplateReader { get; }

        // A local cache of "tags"
        private IDictionary<TagType, TemplateTag> tags;

        // The first time called, reads the template. Subsequent calls just return tags
        private IDictionary<TagType, TemplateTag> Tags
        {
            get
            {
                if(tags != null) return tags;

                tags = new Dictionary<TagType, TemplateTag>();

                foreach (var template in TemplateReader.ReadTemplate())
                {
                    Add(template);
                }
                return tags;
            }
        }

        public TemplateTagCollection(ITemplateReader templateReader)
        {
            TemplateReader = templateReader;
        }

        private void Add(TemplateTag tag)
        {
            try { tags.Add(tag.Type, tag); }
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
