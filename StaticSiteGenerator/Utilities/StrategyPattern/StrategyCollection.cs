using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    public class StrategyCollection<T>
    {
        private IDictionary<string, T> Strategies;

        public StrategyCollection(IEnumerable<T> strategies)
        {
            SetCollection(strategies);
        }

        public void SetCollection(IEnumerable<T> strategies)
        {
            if(strategies == null) throw new ArgumentNullException("Strategies can not be null");

            Strategies = strategies
                .ToDictionary(s => getTypeName(s));
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
