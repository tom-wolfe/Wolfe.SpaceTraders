using System.Net;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Infrastructure.Api.Extensions;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Service.Commands;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure;

internal class SpaceTradersShipService(
    ISpaceTradersApiClient apiClient,
    IShipClient shipClient
) : IShipService
{
    public async Task<Ship?> GetShip(ShipId shipId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetShip(shipId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain(shipClient);
    }

    public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default)
    {
        return PaginationHelpers.ToAsyncEnumerable<SpaceTradersShip>(
            async p => (await apiClient.GetShips(20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain(shipClient)));
    }

    public async Task<PurchaseShipResult> PurchaseShip(PurchaseShipCommand command, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.PurchaseShip(command.ToApi(), cancellationToken);
        return response.GetContent().ToDomain(shipClient);
    }
}