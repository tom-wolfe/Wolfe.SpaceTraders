using Wolfe.SpaceTraders.Domain.Fleet.Commands;
using Wolfe.SpaceTraders.Sdk.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class PurchaseShipRequestExtensions
{
    public static SpaceTradersPurchaseShipRequest ToApi(this PurchaseShipCommand command) => new()
    {
        Waypoint = command.ShipyardId.Value,
        ShipType = command.ShipType.Value,
    };
}