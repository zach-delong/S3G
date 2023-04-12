using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Utilities.DependencyInjection;
using Xunit;

namespace StaticSiteGenerator.UnitTests.DependencyInjection;

public class DecoratorRegistererTests
{
    [Fact]
    public void DecoratorShouldOverrideExistingTypes()
    {
        ServiceCollection serviceCollection = GetServiceCollection();
        serviceCollection.Decorate<ITargetDecorator, TargetDecorator>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var result = serviceProvider.GetService(typeof(ITargetDecorator));

        result.GetType()
            .Should()
            .Be<TargetDecorator>();
    }

    [Fact]
    public void DecoratorShouldBeChainable()
    {
        ServiceCollection sc = GetServiceCollection();
        sc.Decorate<ITargetDecorator, TargetDecorator>();
        sc.Decorate<ITargetDecorator, TargetSecondDecorator>();

        var sp = sc.BuildServiceProvider();

        var result = sp.GetService(typeof(ITargetDecorator));

        Assert.IsType<TargetSecondDecorator>(result);
        result.GetType()
	    .Should()
	    .Be<TargetSecondDecorator>();
    }

    [Fact]
    public void ChangingOrderOfDecoratorsShouldChangeResultingType()
    {

        ServiceCollection sc = GetServiceCollection();
        sc.Decorate<ITargetDecorator, TargetSecondDecorator>();
        sc.Decorate<ITargetDecorator, TargetDecorator>();

        var sp = sc.BuildServiceProvider();

        var result = sp.GetService(typeof(ITargetDecorator));

        result.GetType()
            .Should()
            .Be<TargetDecorator>();
    }

    [Fact]
    public void DecorationShouldCorrectlyInjectDecoratorsWithArbitraryDependencies()
    {

        ServiceCollection sc = GetServiceCollection();
        sc.Decorate<ITargetDecorator, DecoratorWithNewDependencies>();

        var sp = sc.BuildServiceProvider();

        var result = sp.GetService(typeof(ITargetDecorator));

        result.GetType()
            .Should()
            .Be<DecoratorWithNewDependencies>();
    }

    [Fact]
    public void ShouldThrowExceptionWhenOverloadingTypeIsNotRegistered()
    {
        ServiceCollection sc = new ServiceCollection();
        sc
	    .Invoking(s => s.Decorate<ITargetDecorator, DecoratorWithNewDependencies>())
	    .Should()
	    .Throw<Exception>();
    }

    private static ServiceCollection GetServiceCollection()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<ITargetDecorator, TargetBase>();
        serviceCollection.AddTransient<ArbitraryDependency>();
        return serviceCollection;
    }

    private interface ITargetDecorator
    { }

    private class TargetBase : ITargetDecorator
    { }

    private class TargetDecorator : ITargetDecorator
    {
        public TargetDecorator(ITargetDecorator b)
        { }
    }

    private class TargetSecondDecorator : ITargetDecorator
    {
        public TargetSecondDecorator(ITargetDecorator b)
        { }
    }

    private class DecoratorWithNewDependencies : ITargetDecorator
    {
        public DecoratorWithNewDependencies(ITargetDecorator a, ArbitraryDependency b)
        { }
    }

    private class ArbitraryDependency
    { }
}
