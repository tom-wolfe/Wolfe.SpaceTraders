﻿using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal interface ISpaceTradersDataClient
{
    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken);
    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default);
    public Task AddShipyard(Shipyard shipyard, CancellationToken cancellationToken = default);
    public Task AddSystem(StarSystem system, CancellationToken cancellationToken = default);
    public Task AddWaypoint(Waypoint waypoint, CancellationToken cancellationToken = default);
    public Task<string?> GetAccessToken(CancellationToken cancellationToken = default);
    public Task<DataItemResponse<MarketData>?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken);
    public Task<DataItemResponse<Marketplace>?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<DataItemResponse<Marketplace>>? GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default);
    public Task<DataItemResponse<Shipyard>?> GetShipyard(WaypointId shipyardId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<DataItemResponse<Shipyard>>? GetShipyards(SystemId systemId, CancellationToken cancellationToken = default);
    public Task<DataItemResponse<StarSystem>?> GetSystem(SystemId systemId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<DataItemResponse<StarSystem>>? GetSystems(CancellationToken cancellationToken = default);
    public Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Waypoint>? GetWaypoints(SystemId systemId, CancellationToken cancellationToken = default);
    public Task SetAccessToken(string token, CancellationToken cancellationToken = default);
}