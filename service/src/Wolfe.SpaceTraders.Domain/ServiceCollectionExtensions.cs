using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Missions.Logs;

namespace Wolfe.SpaceTraders.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IMarketPriorityService, MarketPriorityService>()
            .AddSingleton<IMissionLogFactory, MissionLogFactory>()
            .AddSingleton<IMissionService, MissionService>();
    }
}