using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Service.Commands;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Domain.Fleet;

public interface IShipService
{
    public Task<Ship?> GetShip(ShipId shipId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default);
    public Task<PurchaseShipResult> PurchaseShip(PurchaseShipCommand command, CancellationToken cancellationToken = default);
}