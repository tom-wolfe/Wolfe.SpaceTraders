using Wolfe.SpaceTraders.Domain.Shipyards;

namespace Wolfe.SpaceTraders.Domain.Exploration;

public interface IExplorationService
{
    public Task<Marketplace.Marketplace?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Marketplace.Marketplace> GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default);
    public Task<Shipyard?> GetShipyard(WaypointId shipyardId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Shipyard> GetShipyards(SystemId systemId, CancellationToken cancellationToken = default);
    public Task<StarSystem?> GetSystem(SystemId systemId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<StarSystem> GetSystems(CancellationToken cancellationToken = default);
    public Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Waypoint> GetWaypoints(SystemId systemId, CancellationToken cancellationToken = default);
}