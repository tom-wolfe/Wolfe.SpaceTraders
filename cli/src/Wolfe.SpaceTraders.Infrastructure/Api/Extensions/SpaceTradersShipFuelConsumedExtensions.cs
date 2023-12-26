using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipFuelConsumedExtensions
{
    public static ShipFuelConsumed ToDomain(this SpaceTradersShipFuelConsumed consumed) => new()
    {
        Amount = new Fuel(consumed.Amount),
        Timestamp = consumed.Timestamp,
    };
}