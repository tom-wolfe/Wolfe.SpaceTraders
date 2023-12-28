using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal interface ISpaceTradersDataClient
{
    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default);
    public Task AddWaypoint(Waypoint waypoint, CancellationToken cancellationToken = default);
    public Task<string?> GetAccessToken(CancellationToken cancellationToken = default);
    public Task<DataItemResponse<Marketplace>?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<DataItemResponse<Marketplace>>? GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default);
    public Task<DataItemResponse<Waypoint>?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<DataItemResponse<Waypoint>>? GetWaypoints(SystemId systemId, CancellationToken cancellationToken = default);
    public Task SetAccessToken(string token, CancellationToken cancellationToken = default);
}