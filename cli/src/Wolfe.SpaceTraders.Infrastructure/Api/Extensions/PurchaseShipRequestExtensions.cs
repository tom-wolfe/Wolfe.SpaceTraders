using Wolfe.SpaceTraders.Sdk.Requests;
using Wolfe.SpaceTraders.Service.Commands;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class PurchaseShipRequestExtensions
{
    public static SpaceTradersPurchaseShipRequest ToApi(this PurchaseShipCommand command) => new()
    {
        Waypoint = command.WaypointId.Value,
        ShipType = command.ShipType.Value,
    };
}