using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Navigation;
using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersLocationExtensions
{
    public static WaypointLocation ToDomain(this SpaceTradersWaypointLocation location) => new()
    {
        Type = new WaypointType(location.Type),
        Symbol = new WaypointSymbol(location.Symbol),
        SystemSymbol = new SystemSymbol(location.SystemSymbol),
        Point = new Point(location.X, location.Y),
    };
}