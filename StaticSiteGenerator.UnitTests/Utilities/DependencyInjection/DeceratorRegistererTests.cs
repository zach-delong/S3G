using System;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Utilities.DependencyInjection;
using Xunit;

namespace StaticSiteGenerator.UnitTests.DependencyInjection
{
    public class DeceratorRegistererTests
    {
        [Fact]
        public void DecoratorShouldOverrideExistingTypes()
        {
            ServiceCollection serviceCollection = GetServiceCollection();
            serviceCollection.Decorate<ITargetDecorator, TargetDecorator>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var result = serviceProvider.GetService(typeof(ITargetDecorator));

            Assert.IsType<TargetDecorator>(result);
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
        }

        [Fact]
        public void ChangingOrderOfDecoratorsShouldChangeResultingType()
        {

            ServiceCollection sc = GetServiceCollection();
            sc.Decorate<ITargetDecorator, TargetSecondDecorator>();
            sc.Decorate<ITargetDecorator, TargetDecorator>();

            var sp = sc.BuildServiceProvider();

            var result = sp.GetService(typeof(ITargetDecorator));

            Assert.IsType<TargetDecorator>(result);
        }

        [Fact]
        public void DecorationShouldCorrectlyInjectDecoratorsWithArbitraryDependencies()
        {

            ServiceCollection sc = GetServiceCollection();
            sc.Decorate<ITargetDecorator, DecoratorWithNewDependencies>();

            var sp = sc.BuildServiceProvider();

            var result = sp.GetService(typeof(ITargetDecorator));

            Assert.IsType<DecoratorWithNewDependencies>(result);
        }

        [Fact]
        public void ShouldThrowExceptionWhenOverloadingTypeIsNotRegistered()
        {
            ServiceCollection sc = new ServiceCollection();
            Assert.Throws<Exception>(() => sc.Decorate<ITargetDecorator, DecoratorWithNewDependencies>());
        }

        private static ServiceCollection GetServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ITargetDecorator, TargetBase>();
            serviceCollection.AddTransient<ArbitraryDependency>();
            return serviceCollection;
        }

        private interface ITargetDecorator
        {}

        private class TargetBase: ITargetDecorator
        {}

        private class TargetDecorator : ITargetDecorator
        {
            public TargetDecorator(ITargetDecorator b)
            {}
        }

        private class TargetSecondDecorator: ITargetDecorator
        {
            public TargetSecondDecorator(ITargetDecorator b)
            {}
        }

        private class DecoratorWithNewDependencies: ITargetDecorator
        {
            public DecoratorWithNewDependencies(ITargetDecorator a, ArbitraryDependency b)
            {}
        }

        private class ArbitraryDependency
        {}
    }
}
