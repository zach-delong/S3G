using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Utilities;

public static class ServicesConfiguration
{
    public static void AddUtilities(this IServiceCollection services)
    {
        services.AddTransient(typeof(StrategyCollection<>), typeof(StrategyCollection<>));
        services.AddTransient(typeof(IStrategyExecutor<,>), typeof(GenericStrategyExecutor<,>));
        services.AddTransient<HeaderLevelHelper>();
    }
}
