using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolfe.SpaceTraders.Service.Missions;

namespace Wolfe.SpaceTraders.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IMissionService, MissionService>();
    }
}