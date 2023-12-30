using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
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
using Wolfe.SpaceTraders.Infrastructure.Mongo;
using Wolfe.SpaceTraders.Infrastructure.Ships;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service.Agents;
using Wolfe.SpaceTraders.Service.Ships;

namespace Wolfe.SpaceTraders.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MarketplaceServiceOptions>(configuration.GetSection("Market").Bind);

        return services
            .AddFileDatabase(configuration)
            .AddMongo(configuration)
            .AddSpaceTradersApi(configuration)
            .AddSingleton<IAgentService, SpaceTradersAgentService>()
            .AddSingleton<IExplorationService, SpaceTradersExplorationService>()
            .AddSingleton<IContractService, ContractService>()
            .AddSingleton<IFleetService, SpaceTradersFleetService>()
            .AddSingleton<IMarketplaceService, MarketplaceService>()
            .AddSingleton<IShipClient, SpaceTradersShipClient>()
            .AddSingleton<IShipService, SpaceTradersShipService>()
            .AddSingleton<ITokenService, TokenService>();
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
            });
    }

    private static IServiceCollection AddFileDatabase(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<SpaceTradersDataOptions>(configuration.GetSection("Database").Bind)
        .AddSingleton<ISpaceTradersDataClient, SpaceTradersFileSystemDataClient>();
}