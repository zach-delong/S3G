using System;
using System.Collections.Generic;
using System.ComponentModel;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    [TransientService]
    public class StrategyCollection<T>
    {
        private IDictionary<string, T> Strategies;

        public StrategyCollection(IEnumerable<T> strategies)
        {
            SetCollection(strategies);
        }

        public void SetCollection(IEnumerable<T> strategies)
        {
            Strategies = new Dictionary<string, T>();
            foreach(var strategy in strategies)
            {
                string typeName = getTypeName(strategy);

                Strategies.Add(typeName, strategy);
            }
        }

        private string getTypeName(T strategy)
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

        public virtual T GetConverterForType(Type t)
        {
            if(!Strategies.ContainsKey(t.Name))
            {
                throw new StrategyNotFoundException($"Converter for type {t.Name} not found");
            }

            return Strategies[t.Name];
        }
    }
}
