using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Infrastructure.Data.Responses;

internal class GetWaypointsResponse
{
    public required IReadOnlyCollection<Waypoint> Waypoints { get; init; }
    public required DateTimeOffset RetrievedAt { get; init; }

    public static readonly GetWaypointsResponse None = new()
    {
        Waypoints = Array.Empty<Waypoint>(),
        RetrievedAt = DateTimeOffset.MinValue
    };
}