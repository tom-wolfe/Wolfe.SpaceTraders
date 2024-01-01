using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions.Logs;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Infrastructure.Agents;
using Wolfe.SpaceTraders.Infrastructure.Contracts;
using Wolfe.SpaceTraders.Infrastructure.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Fleet;
using Wolfe.SpaceTraders.Infrastructure.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Missions;
using Wolfe.SpaceTraders.Infrastructure.Mongo;
using Wolfe.SpaceTraders.Infrastructure.Ships;
using Wolfe.SpaceTraders.Infrastructure.Shipyards;
using Wolfe.SpaceTraders.Sdk;

namespace Wolfe.SpaceTraders.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MarketplaceServiceOptions>(configuration.GetSection("Market").Bind);

        return services
            .AddMongo(configuration)
            .AddSpaceTradersApi(configuration)
            .AddSingleton<IAgentService, SpaceTradersAgentService>()
            .AddSingleton<IExplorationService, ExplorationService>()
            .AddSingleton<IContractService, ContractService>()
            .AddSingleton<IFleetService, FleetService>()
            .AddSingleton<IMarketplaceService, MarketplaceService>()
            .AddSingleton<IShipyardService, ShipyardService>()
            .AddSingleton<IShipClient, ShipClient>()
            .AddSingleton<IShipService, ShipService>()
            .AddSingleton<ITokenService, TokenService>()
            .AddSingleton<IMissionLogProvider, MongoMissionLogProvider>()
            .AddSingleton<IMissionLogProvider, LoggerMissionLogProvider>();
    }

    private static IServiceCollection AddSpaceTradersApi(this IServiceCollection services, IConfiguration configuration) => services
        .AddSpaceTraders(o =>
        {
            configuration.GetSection("SpaceTraders").Bind(o);
            o.ApiKeyProvider = async (provider, ct) =>
            {
                var tokenService = provider.GetRequiredService<ITokenService>();
                var token = await tokenService.GetAccessToken(ct);
                return token ?? throw new InvalidOperationException("Missing API token.");
            };
        });

    private static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, _ => true);

        return services
            .Configure<MongoOptions>(configuration.GetSection("Mongo").Bind)
            .AddSingleton<IMongoClient, MongoClient>(p =>
            {
                var options = p.GetRequiredService<IOptions<MongoOptions>>().Value;
                return new MongoClient(options.ConnectionString);
            })
            .AddSingleton<IExplorationStore, MongoExplorationStore>()
            .AddSingleton<IMarketplaceStore, MongoMarketplaceStore>()
            .AddSingleton<IShipyardStore, MongoShipyardStore>();
    }
}