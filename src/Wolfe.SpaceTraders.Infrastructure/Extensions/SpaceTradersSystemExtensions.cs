using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Navigation;
using Wolfe.SpaceTraders.Domain.Models.Systems;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersSystemExtensions
{
    public static StarSystem ToDomain(this SpaceTradersSystem system) => new()
    {
        Symbol = new SystemSymbol(system.Symbol),
        Type = new SystemType(system.Type),
        Factions = system.Factions.Select(f => f.ToDomain()).ToList(),
        SectorSymbol = new SectorSymbol(system.SectorSymbol),
        Waypoints = system.Waypoints.Select(w => w.ToDomain()).ToList(),
        Point = new Point(system.X, system.Y),
    };
}