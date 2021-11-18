using Xunit;
using System;
using StaticSiteGenerator.Utilities.StrategyPattern;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using StaticSiteGenerator.Markdown.InlineElement;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Utilities.StrategyPattern.Exceptions;

namespace StaticSiteGenerator.UnitTests.Utilities.StrategyPattern
{
    public class StrategyCollectionTests
    {
        private class FakeStrategyMapper : StrategyForTypeAttribute
        {
            public FakeStrategyMapper(string typeName) : base(typeName)
            {
            }
        }

        [FakeStrategyMapper(nameof(Object))]
        private class FakeConverterWithAttribute : IInlineElementConverter
        {
            public IInlineElement Execute(IInline inline)
            {
                throw new NotImplementedException();
            }
        }

        private class FakeConverterWithoutAttribute : IInlineElementConverter
        {
            public IInlineElement Execute(IInline inline)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void SetCollectionShouldMapTypesToImplementations()
        {
            var strategyCollection = new StrategyCollection<IInlineElementConverter>(new List<IInlineElementConverter>());
            var mockConverter = new FakeConverterWithAttribute();
            object dummyObject = new Object();

            strategyCollection.SetCollection(new List<IInlineElementConverter>() {
                    mockConverter
            });

            var strategy = strategyCollection.GetStrategyForType(dummyObject.GetType());

            Assert.IsType(mockConverter.GetType(), strategy);
        }

        [Fact]
        public void SetCollectionShouldPassOnEmptyList()
        {
            var strategyCollection = new StrategyCollection<IInlineElementConverter>(new List<IInlineElementConverter>());

            var exception = Record.Exception(() => strategyCollection.SetCollection(new List<IInlineElementConverter>()));

            Assert.Null(exception);
        }

        [Fact]
        void SetCollectionShouldFailOnNullList()
        {
            var strategyCollection = new StrategyCollection<IInlineElementConverter>(new List<IInlineElementConverter>());

            Assert.Throws<ArgumentNullException>(() => strategyCollection.SetCollection(null));
        }

        [Fact]
        void SetCollectionShouldThrowErrorOnTypeWithNoAttribute()
        {
            var strategyCollection = new StrategyCollection<IInlineElementConverter>(new List<IInlineElementConverter>());
            var mockConverter = new FakeConverterWithoutAttribute();
            object dummyObject = new Object();

            Assert.Throws<StrategyMapperAttributeNotFoundException>(() =>
            {
                strategyCollection.SetCollection(new List<IInlineElementConverter>() {
                        mockConverter
                });
            });
        }

        [Fact]
        void GetStrategyShouldThrowExceptionWhenTypeIsNotFound()
        {
            var strategyCollection = new StrategyCollection<IInlineElementConverter>(new List<IInlineElementConverter>());
            var mockConverter = new FakeConverterWithAttribute();
            object dummyObject = new Object();

            strategyCollection.SetCollection(new List<IInlineElementConverter>() {
                    mockConverter
            });

            Assert.Throws<StrategyNotFoundException>(() =>
            {
                var strategy = strategyCollection.GetStrategyForType(mockConverter.GetType());
            });

        }
    }
}
