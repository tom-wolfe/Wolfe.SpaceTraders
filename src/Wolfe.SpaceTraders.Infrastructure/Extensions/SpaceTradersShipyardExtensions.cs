using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Sdk.Models.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipyardExtensions
{
    public static Shipyard ToDomain(this SpaceTradersShipyard shipyard) => new()
    {
        Symbol = new WaypointSymbol(shipyard.Symbol),
        ShipTypes = shipyard.ShipTypes.Select(s => s.ToDomain()).ToList(),
        Ships = shipyard.Ships.Select(s => s.ToDomain()).ToList(),
        Transactions = shipyard.Transactions.Select(s => s.ToDomain()).ToList(),
    };
}