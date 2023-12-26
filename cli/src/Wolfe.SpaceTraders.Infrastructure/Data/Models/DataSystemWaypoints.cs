namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataSystemWaypoints
{
    public DateTimeOffset RetrievedAt { get; init; }
    public required IReadOnlyCollection<DataWaypoint> Waypoints { get; init; }
}
