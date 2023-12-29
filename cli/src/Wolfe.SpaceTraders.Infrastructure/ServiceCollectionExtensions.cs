using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Agents;
using Wolfe.SpaceTraders.Infrastructure.Contracts;
using Wolfe.SpaceTraders.Infrastructure.Data;
using Wolfe.SpaceTraders.Infrastructure.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Fleet;
using Wolfe.SpaceTraders.Infrastructure.Ships;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service.Agents;
using Wolfe.SpaceTraders.Service.Contracts;
using Wolfe.SpaceTraders.Service.Ships;

namespace Wolfe.SpaceTraders.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<SpaceTradersDataOptions>()
            .Configure(configuration.GetSection("Database").Bind);

        return services
            .AddSpaceTradersApi(o =>
            {
                configuration.GetSection("SpaceTraders").Bind(o);
                o.ApiKeyProvider = async (provider, ct) =>
                {
                    var tokenService = provider.GetRequiredService<ITokenService>();
                    var token = await tokenService.GetAccessToken(ct);
                    return token ?? throw new InvalidOperationException("Missing API token.");
                };
            })
            .AddSingleton<ISpaceTradersDataClient, SpaceTradersFileSystemDataClient>()
            .AddSingleton<IAgentService, SpaceTradersAgentService>()
            .AddSingleton<IExplorationService, SpaceTradersExplorationService>()
            .AddSingleton<IContractClient, SpaceTradersContractClient>()
            .AddSingleton<IContractService, SpaceTradersContractService>()
            .AddSingleton<IFleetService, SpaceTradersFleetService>()
            .AddSingleton<IShipClient, SpaceTradersShipClient>()
            .AddSingleton<IShipService, SpaceTradersShipService>()
            .AddSingleton<ITokenService, TokenService>();
    }
}