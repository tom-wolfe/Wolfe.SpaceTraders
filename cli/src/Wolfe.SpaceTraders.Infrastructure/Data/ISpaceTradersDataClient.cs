using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Data.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal interface ISpaceTradersDataClient
{
    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default);
    public Task AddWaypoints(SystemSymbol systemId, IEnumerable<Waypoint> waypoints, CancellationToken cancellationToken = default);
    public Task<DataItemResponse<Marketplace>> GetMarketplace(WaypointSymbol marketplaceId, CancellationToken cancellationToken = default);
    public Task<DataListResponse<Waypoint>> GetWaypoints(SystemSymbol systemId, CancellationToken cancellationToken = default);
}