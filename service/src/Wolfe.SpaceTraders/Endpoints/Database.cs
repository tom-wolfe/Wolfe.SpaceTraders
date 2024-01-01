using Wolfe.SpaceTraders.Infrastructure.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Shipyards;

namespace Wolfe.SpaceTraders.Endpoints;

public static class Database
{
    public static WebApplication MapDatabaseEndpoints(this WebApplication app)
    {
        var databaseGroup = app.MapGroup("/database");
        databaseGroup.MapPost("/reset", (IExplorationStore explorationStore, IMarketplaceStore marketplaceStore, IShipyardStore shipyardStore, CancellationToken cancellationToken = default) =>
            Task.WhenAll(
                explorationStore.Clear(cancellationToken),
                marketplaceStore.Clear(cancellationToken),
                shipyardStore.Clear(cancellationToken)
            )
        );

        return app;
    }
}