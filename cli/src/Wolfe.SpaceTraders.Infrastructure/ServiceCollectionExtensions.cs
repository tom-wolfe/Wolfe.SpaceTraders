using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Data;
using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service;

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
                    var token = await tokenService.Read(ct);
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
            .AddSingleton<ITokenService, FileTokenService>();
    }
}