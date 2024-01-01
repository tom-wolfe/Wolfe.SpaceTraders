using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Endpoints;

public static class Marketplaces
{
    public static WebApplication MapMarketplaceEndpoints(this WebApplication app)
    {
        app.MapGet("/systems/{systemId}/marketplaces", (IMarketplaceService marketplaceService, SystemId systemId, CancellationToken cancellationToken = default) => marketplaceService.GetMarketplaces(systemId, cancellationToken));

        app.MapGet("/marketplaces/{marketplaceId}", async (IMarketplaceService marketplaceService, WaypointId marketplaceId, CancellationToken cancellationToken = default) =>
        {
            var marketplace = await marketplaceService.GetMarketplace(marketplaceId, cancellationToken);
            return marketplace == null ? Results.NotFound() : Results.Ok(marketplace);
        });

        return app;
    }
}
