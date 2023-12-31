using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Infrastructure.Exploration;

public interface IExplorationStore
{
    public Task AddSystem(StarSystem system, CancellationToken cancellationToken = default);
    public Task AddWaypoint(Waypoint waypoint, CancellationToken cancellationToken = default);
    public Task<StarSystem?> GetSystem(SystemId systemId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<StarSystem> GetSystems(CancellationToken cancellationToken = default);
    public Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Waypoint> GetWaypoints(SystemId systemId, CancellationToken cancellationToken = default);
    public Task Clear(CancellationToken cancellationToken = default);
}