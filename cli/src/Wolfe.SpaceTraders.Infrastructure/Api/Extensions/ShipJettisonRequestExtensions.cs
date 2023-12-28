using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Sdk.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class ShipJettisonRequestExtensions
{
    public static SpaceTradersShipJettisonRequest ToApi(this ShipJettisonCommand command) => new()
    {
        Symbol = command.ItemId.Value,
        Units = command.Quantity
    };
}