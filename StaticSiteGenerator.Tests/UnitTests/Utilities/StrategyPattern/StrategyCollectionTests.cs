using Xunit;
using System;
using StaticSiteGenerator.Utilities.StrategyPattern;
using System.Collections.Generic;
using StaticSiteGenerator.Utilities.StrategyPattern.Exceptions;
using StaticSiteGenerator.Files;
using FluentAssertions;

namespace StaticSiteGenerator.UnitTests.Utilities.StrategyPattern;

public class StrategyCollectionTests
{
    private class FakeStrategyMapper : StrategyForTypeAttribute
    {
        public FakeStrategyMapper(string typeName) : base(typeName)
        {
        }
    }

    [FakeStrategyMapper(nameof(Object))]
    private class FakeConverterWithAttribute : IStrategy<object, IFileSystemObject>
    {
        public object Execute(IFileSystemObject input)
        {
            throw new NotImplementedException();
        }
    }

    private class FakeConverterWithoutAttribute : IStrategy<object, IFileSystemObject>
    {
        public object Execute(IFileSystemObject input)
        {
            throw new NotImplementedException();
        }
    }

    [Fact]
    public void SetCollectionShouldMapTypesToImplementations()
    {
        var strategyCollection = new StrategyCollection<IStrategy<object, IFileSystemObject>>(new List<IStrategy<object, IFileSystemObject>>());
        var mockConverter = new FakeConverterWithAttribute();
        object dummyObject = new Object();

        strategyCollection.SetCollection(new List<IStrategy<object, IFileSystemObject>>() {
                    mockConverter
            });

        var strategy = strategyCollection.GetStrategyForType(dummyObject.GetType());

        strategy
            .GetType()
            .Should()
            .Be(mockConverter.GetType());
    }

    [Fact]
    public void SetCollectionShouldPassOnEmptyList()
    {
        var strategyCollection = new StrategyCollection<IStrategy<object, IFileSystemObject>>(new List<IStrategy<object, IFileSystemObject>>());

        var exception = Record.Exception(() => strategyCollection.SetCollection(new List<IStrategy<object, IFileSystemObject>>()));

        exception
            .Should()
            .BeNull();
    }

    [Fact]
    void SetCollectionShouldFailOnNullList()
    {
        var strategyCollection = new StrategyCollection<IStrategy<object, IFileSystemObject>>(new List<IStrategy<object, IFileSystemObject>>());

        strategyCollection
	    .Invoking(c => c.SetCollection(null))
	    .Should()
	    .Throw<ArgumentNullException>();
    }

    [Fact]
    void SetCollectionShouldThrowErrorOnTypeWithNoAttribute()
    {
        var strategyCollection = new StrategyCollection<IStrategy<object, IFileSystemObject>>(new List<IStrategy<object, IFileSystemObject>>());
        var mockConverter = new FakeConverterWithoutAttribute();
        object dummyObject = new Object();

	strategyCollection.Invoking(sc =>
	    sc.SetCollection(new List<IStrategy<object, IFileSystemObject>>() {
		mockConverter
	    }))
	    .Should()
	    .Throw<StrategyMapperAttributeNotFoundException>();
    }

    [Fact]
    void GetStrategyShouldThrowExceptionWhenTypeIsNotFound()
    {
        var strategyCollection = new StrategyCollection<IStrategy<object, IFileSystemObject>>(new List<IStrategy<object, IFileSystemObject>>());
        var mockConverter = new FakeConverterWithAttribute();
        object dummyObject = new Object();

        strategyCollection.SetCollection(new List<IStrategy<object, IFileSystemObject>>() {
                    mockConverter
            });

        strategyCollection
	    .Invoking(sc => sc.GetStrategyForType(mockConverter.GetType()))
	    .Should()
	    .Throw<StrategyNotFoundException>();

    }
}
