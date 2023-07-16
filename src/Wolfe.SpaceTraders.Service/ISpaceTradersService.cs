using Wolfe.SpaceTraders.Core.Responses;

namespace Wolfe.SpaceTraders.Service;

public interface ISpaceTradersService
{
    public Task<PurchaseShipResponse> PurchaseFirstShip(CancellationToken cancellationToken = default);
}