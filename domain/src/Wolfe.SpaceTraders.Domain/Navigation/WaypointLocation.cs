using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Navigation;

public class WaypointLocation
{
    public required WaypointId Id { get; init; }
    public required WaypointType Type { get; init; }
    public required SystemId SystemId { get; init; }
    public required Point Point { get; init; }
}