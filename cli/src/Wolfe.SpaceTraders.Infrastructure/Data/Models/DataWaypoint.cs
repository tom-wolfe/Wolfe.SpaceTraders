namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataWaypoint
{
    public required string Symbol { get; init; }
    public required string Type { get; init; }
    public required string System { get; init; }
    public required DataPoint Location { get; init; }
    public required IReadOnlyCollection<DataWaypointTrait> Traits { get; init; } = [];
}
