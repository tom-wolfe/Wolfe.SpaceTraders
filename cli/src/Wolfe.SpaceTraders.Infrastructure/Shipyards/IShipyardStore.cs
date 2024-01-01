using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Shipyards;

namespace Wolfe.SpaceTraders.Infrastructure.Shipyards;

public interface IShipyardStore
{
    public Task AddShipyard(Shipyard shipyard, CancellationToken cancellationToken = default);
    public Task<Shipyard?> GetShipyard(WaypointId shipyardId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Shipyard> GetShipyards(SystemId systemId, CancellationToken cancellationToken = default);
    public Task Clear(CancellationToken cancellationToken = default);
}