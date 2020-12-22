using System;
using System.Collections.Generic;
using System.ComponentModel;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    [TransientService]
    public class StrategyCollection
    {
        private IDictionary<string, IInlineElementConverter> Strategies;

        public void SetCollection(IEnumerable<IInlineElementConverter> strategies)
        {
            Strategies = new Dictionary<string, IInlineElementConverter>();
            foreach(var strategy in strategies)
            {
                string typeName = getTypeName(strategy);

                Strategies.Add(typeName, strategy);
            }
        }

        private string getTypeName(IInlineElementConverter strategy)
        {
            var type = strategy.GetType();

            StrategyForTypeAttribute attribute = (StrategyForTypeAttribute)TypeDescriptor
                .GetAttributes(strategy)[typeof(StrategyForTypeAttribute)];

            if(attribute == null)
            {
                throw new StrategyMapperAttributeNotFoundException($"on type {type.Name}");
            }

            return attribute.TypeName;

        }

        public IInlineElementConverter GetConverterForType(Type t)
        {
            if(!Strategies.ContainsKey(t.Name))
            {
                throw new StrategyNotFoundException($"Converter for type {t.Name} not found");
            }

            return Strategies[t.Name];
        }
    }
}
