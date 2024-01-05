using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Shipyards;

namespace Wolfe.SpaceTraders.Endpoints;

public static class Shipyards
{
    public static WebApplication MapShipyardEndpoints(this WebApplication app)
    {
        app.MapGet("/systems/{systemId}/shipyards", (IShipyardService shipyardService, SystemId systemId, CancellationToken cancellationToken = default) => shipyardService.GetShipyards(systemId, cancellationToken));

        app.MapGet("/shipyards/{shipyardId}", async (IShipyardService shipyardService, WaypointId shipyardId, CancellationToken cancellationToken = default) =>
        {
            var shipyard = await shipyardService.GetShipyard(shipyardId, cancellationToken);
            return shipyard == null ? Results.NotFound() : Results.Ok(shipyard);
        });

        return app;
    }
}
