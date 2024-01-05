using System.Net;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Fleet.Commands;
using Wolfe.SpaceTraders.Domain.Fleet.Results;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

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

    public async Task<Ship?> GetShip(ShipId shipId, CancellationToken cancellationToken = default)
    {
        var me = (await apiClient.GetAgent(cancellationToken)).GetContent().Data;
        var response = await apiClient.GetShip(shipId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain(new AgentId(me.Symbol), shipClient);
    }

    public async IAsyncEnumerable<Ship> GetShips([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var me = (await apiClient.GetAgent(cancellationToken)).GetContent().Data;
        var results = PaginationHelpers.ToAsyncEnumerable<SpaceTradersShip>(async p => (await apiClient.GetShips(20, p, cancellationToken)).GetContent());
        await foreach (var result in results)
        {
            yield return result.ToDomain(new AgentId(me.Symbol), shipClient);
        }
    }
}
