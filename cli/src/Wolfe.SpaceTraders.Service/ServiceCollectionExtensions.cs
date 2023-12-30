using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Service.Exploration;
using Wolfe.SpaceTraders.Service.Markets;
using Wolfe.SpaceTraders.Service.Missions;

namespace Wolfe.SpaceTraders.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IMarketPriorityService, MarketPriorityService>()
            .AddSingleton<IMissionService, MissionService>()
            .AddSingleton<IWayfinderService, AStarWayfinderService>()
            .AddSingleton<IMissionLogFactory, ConsoleMissionLogFactory>();
    }
}