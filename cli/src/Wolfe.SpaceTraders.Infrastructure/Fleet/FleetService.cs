using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Fleet.Commands;
using Wolfe.SpaceTraders.Domain.Fleet.Results;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Sdk;

namespace Wolfe.SpaceTraders.Infrastructure.Fleet;

internal class FleetService(
    ISpaceTradersApiClient apiClient,
    IShipClient shipClient
) : IFleetService
{
    public async Task<PurchaseShipResult> PurchaseShip(PurchaseShipCommand command, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.PurchaseShip(command.ToApi(), cancellationToken);
        return response.GetContent().ToDomain(shipClient);
    }
}