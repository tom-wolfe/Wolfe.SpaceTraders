using Wolfe.SpaceTraders.Domain.Fleet.Commands;
using Wolfe.SpaceTraders.Domain.Fleet.Results;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Fleet;

public interface IFleetService
{
    public Task<PurchaseShipResult> PurchaseShip(PurchaseShipCommand command, CancellationToken cancellationToken = default);
    public Task<Ship?> GetShip(ShipId shipId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default);
}