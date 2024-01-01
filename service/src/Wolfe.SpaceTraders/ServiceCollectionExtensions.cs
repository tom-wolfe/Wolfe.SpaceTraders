using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IWayfinderService, AStarWayfinderService>();
    }
}