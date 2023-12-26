using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal interface ISpaceTradersDataClient
{
    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default);
    public Task AddWaypoint(Waypoint waypoint, CancellationToken cancellationToken = default);
    public Task<DataItemResponse<Marketplace>?> GetMarketplace(WaypointSymbol marketplaceId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<DataItemResponse<Marketplace>>? GetMarketplaces(SystemSymbol systemId, CancellationToken cancellationToken = default);
    public Task<DataItemResponse<Waypoint>?> GetWaypoint(WaypointSymbol waypointId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<DataItemResponse<Waypoint>>? GetWaypoints(SystemSymbol systemId, CancellationToken cancellationToken = default);
}