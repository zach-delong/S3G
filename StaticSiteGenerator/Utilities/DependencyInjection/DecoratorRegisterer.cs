using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace StaticSiteGenerator.Utilities.DependencyInjection;

public static class DecoratorRegisterer
{
    // Given an Interface (an interface to decorate) and a Decorator,
    // Replace the previously registered Interface with one wrapped by
    // the provided Decorator.
    // Implemented based on https://greatrexpectations.com/2018/10/25/decorators-in-net-core-with-dependency-injection
    // Really seems like something about this should be built in/distributed as an extension
    public static void Decorate<Interface, Decorator>(this IServiceCollection services)
        where Interface : class
        where Decorator : class, Interface
    {
        var existingImplementation = services.FirstOrDefault(s => s.ServiceType == typeof(Interface));

        if (existingImplementation == null)
        {
            throw new Exception($"{typeof(Interface).Name} is not registered. Did you register it before trying to decorate it?");
        }

        var factory = ActivatorUtilities.CreateFactory(typeof(Decorator), new[] { typeof(Interface) });

        services.Replace(ServiceDescriptor.Describe(
                             typeof(Interface),
                             s => (Interface)factory(s, new[] { s.CreateInstance(existingImplementation) }),
                             existingImplementation.Lifetime));
    }
}
