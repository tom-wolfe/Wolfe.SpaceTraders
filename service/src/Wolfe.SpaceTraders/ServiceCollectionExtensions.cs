using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;
using Wolfe.SpaceTraders.Services;
using Wolfe.SpaceTraders.Services.Exploration;

namespace Wolfe.SpaceTraders;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IWayfinderService, AStarWayfinderService>()
            .AddSingleton<IMissionScheduler, BackgroundServiceMissionScheduler>()
            .AddHostedService<MissionManagerService>();
    }
}