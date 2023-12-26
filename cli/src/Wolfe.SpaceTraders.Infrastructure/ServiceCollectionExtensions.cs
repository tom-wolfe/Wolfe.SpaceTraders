using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSpaceTradersApi(configuration.GetSection("SpaceTraders").Bind)
            .AddSingleton<ISpaceTradersClient, SpaceTradersClient>();
    }
}