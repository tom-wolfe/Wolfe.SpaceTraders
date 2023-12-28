using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipyardExtensions
{
    public static Shipyard ToDomain(this SpaceTradersShipyard shipyard, Waypoint waypoint) => new()
    {
        Id = new WaypointId(shipyard.Symbol),
        Type = waypoint.Type,
        Location = waypoint.Location,
        Traits = [.. waypoint.Traits],
        ShipTypes = shipyard.ShipTypes.Select(s => s.ToDomain()).ToList(),
        Ships = shipyard.Ships.Select(s => s.ToDomain()).ToList(),
        Transactions = shipyard.Transactions.Select(s => s.ToDomain()).ToList(),
    };
}