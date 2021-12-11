using System;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.Utilities.DependencyInjection;

// Given a DI context and a service descriptor, figure out how to instantiate the type
// Implemented based on https://greatrexpectations.com/2018/10/25/decorators-in-net-core-with-dependency-injection
public static class InstanceCreator
{
    public static object CreateInstance(this IServiceProvider services, ServiceDescriptor descriptor)
    {
        if (descriptor.ImplementationInstance != null)
            return descriptor.ImplementationInstance;

        if (descriptor.ImplementationFactory != null)
            return descriptor.ImplementationFactory(services);

        return ActivatorUtilities.GetServiceOrCreateInstance(services, descriptor.ImplementationType);
    }
}
