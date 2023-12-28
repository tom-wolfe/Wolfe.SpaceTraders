namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataWaypoint
{
    public required string Id { get; init; }
    public required string Type { get; init; }
    public required DataPoint Location { get; init; }
    public required IReadOnlyCollection<DataWaypointTrait> Traits { get; init; } = [];
}
