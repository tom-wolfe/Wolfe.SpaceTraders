using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Agents;
using Wolfe.SpaceTraders.Infrastructure.Contracts;
using Wolfe.SpaceTraders.Infrastructure.Data;
using Wolfe.SpaceTraders.Infrastructure.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Fleet;
using Wolfe.SpaceTraders.Infrastructure.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Ships;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service.Agents;
using Wolfe.SpaceTraders.Service.Ships;

namespace Wolfe.SpaceTraders.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<SpaceTradersDataOptions>(configuration.GetSection("Database").Bind)
            .Configure<MarketServiceOptions>(configuration.GetSection("Market").Bind);

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
            .AddSingleton<IContractService, ContractService>()
            .AddSingleton<IFleetService, SpaceTradersFleetService>()
            .AddSingleton<IMarketplaceService, MarketplaceService>()
            .AddSingleton<IShipClient, SpaceTradersShipClient>()
            .AddSingleton<IShipService, SpaceTradersShipService>()
            .AddSingleton<ITokenService, TokenService>();
    }
}