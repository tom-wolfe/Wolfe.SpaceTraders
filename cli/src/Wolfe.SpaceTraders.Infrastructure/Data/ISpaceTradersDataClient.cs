using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Data.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal interface ISpaceTradersDataClient
{
    public Task AddWaypoints(SystemSymbol systemId, IEnumerable<Waypoint> waypoints, CancellationToken cancellationToken = default);
    public Task<GetWaypointsResponse> GetWaypoints(SystemSymbol systemId, CancellationToken cancellationToken = default);
}