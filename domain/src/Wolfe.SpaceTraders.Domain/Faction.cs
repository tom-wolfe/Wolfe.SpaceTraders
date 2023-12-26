using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain;

public class Faction
{
    public FactionSymbol Symbol { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required WaypointSymbol Headquarters { get; init; }
}