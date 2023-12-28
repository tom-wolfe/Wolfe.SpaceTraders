using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Sdk.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class ShipNavigateRequestExtensions
{
    public static SpaceTradersShipNavigateRequest ToApi(this ShipNavigateCommand command) => new()
    {
        WaypointSymbol = command.WaypointId.Value,
    };
}