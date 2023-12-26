using System.Net;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Api.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Data;
using Wolfe.SpaceTraders.Infrastructure.Data.Responses;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;
using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Systems;
using Wolfe.SpaceTraders.Sdk.Requests;
using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service;
using Wolfe.SpaceTraders.Service.Commands;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure;

internal class SpaceTradersClient(
    ISpaceTradersApiClient apiClient,
    ISpaceTradersDataClient dataClient
) : ISpaceTradersClient
{
    public async Task<RegisterResult> Register(RegisterCommand command, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.Register(command.ToApi(), cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<Agent> GetAgent(CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetAgent(cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default)
    {
        return AsyncEnumerate<SpaceTradersContract>(
            async p => (await apiClient.GetContracts(20, p, cancellationToken)).GetContent()
        ).SelectAwait(c => ValueTask.FromResult(c.ToDomain()));
    }

    public async Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetContract(contractId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain();
    }

    public async Task<AcceptContractResult> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.AcceptContract(contractId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<Shipyard?> GetShipyard(WaypointSymbol waypointId, CancellationToken cancellationToken = default)
    {
        var waypoint = await GetWaypoint(waypointId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await apiClient.GetShipyard(waypointId.System.Value, waypointId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain(waypoint);
    }

    public async Task<PurchaseShipResult> PurchaseShip(PurchaseShipCommand command, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.PurchaseShip(command.ToApi(), cancellationToken);
        return response.GetContent().ToDomain();
    }

    public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default)
    {
        return AsyncEnumerate<SpaceTradersShip>(
            async p => (await apiClient.GetShips(20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));
    }

    public async Task<Ship?> GetShip(ShipSymbol shipId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetShip(shipId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain();
    }

    public async Task<SetShipSpeedResult> SetShipSpeed(ShipSymbol shipId, FlightSpeed speed, CancellationToken cancellationToken = default)
    {
        var request = new SpaceTradersPatchShipNavRequest { FlightMode = speed.Value };
        var response = await apiClient.PatchShipNav(shipId.Value, request, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipOrbitResult> ShipOrbit(ShipSymbol shipId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.ShipOrbit(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipDockResult> ShipDock(ShipSymbol shipId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.ShipDock(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipNavigateResult> ShipNavigate(ShipSymbol shipId, ShipNavigateCommand command, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.ShipNavigate(shipId.Value, command.ToApi(), cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipRefuelResult> ShipRefuel(ShipSymbol shipId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.ShipRefuel(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipSellResult> ShipSell(ShipSymbol shipId, ShipSellCommand command, CancellationToken cancellationToken = default)
    {
        var request = new SpaceTradersShipSellRequest
        {
            Symbol = command.ItemId.Value,
            Units = command.Quantity
        };
        var response = await apiClient.ShipSell(shipId.Value, request, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<Market?> GetMarket(WaypointSymbol waypointId, CancellationToken cancellationToken = default)
    {
        var waypoint = await GetWaypoint(waypointId, cancellationToken);
        if (waypoint == null) { return null; }
        var response = await apiClient.GetMarket(waypointId.System.Value, waypointId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain(waypoint);
    }

    public async Task<ShipExtractResult> ShipExtract(ShipSymbol shipId, CancellationToken cancellationToken)
    {
        var response = await apiClient.ShipExtract(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public IAsyncEnumerable<StarSystem> GetSystems(CancellationToken cancellationToken = default)
    {
        return AsyncEnumerate<SpaceTradersSystem>(
            async p => (await apiClient.GetSystems(20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));
    }

    public async Task<StarSystem?> GetSystem(SystemSymbol systemId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetSystem(systemId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain();
    }

    public async IAsyncEnumerable<Waypoint> GetWaypoints(SystemSymbol systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var cached = await dataClient.GetWaypoints(systemId, cancellationToken);
        if (cached != GetWaypointsResponse.None)
        {
            foreach (var waypoint in cached.Waypoints)
            {
                yield return waypoint;
            }
        }

        var waypoints = await AsyncEnumerate<SpaceTradersWaypoint>(
            async p => (await apiClient.GetWaypoints(systemId.Value, null, [], 20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()))
        .ToListAsync(cancellationToken);

        await dataClient.AddWaypoints(systemId, waypoints, cancellationToken);

        foreach (var waypoint in waypoints)
        {
            yield return waypoint;
        }
    }

    public async Task<Waypoint?> GetWaypoint(WaypointSymbol waypointId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetWaypoint(waypointId.System.Value, waypointId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain();
    }

    private static async IAsyncEnumerable<T> AsyncEnumerate<T>(Func<int, Task<ISpaceTradersListResponse<T>>> getPage)
    {
        var page = 1;
        var count = 0;
        int total;

        do
        {
            var result = await getPage(page);

            count += result.Meta.Limit;
            total = result.Meta.Total;
            page++;

            foreach (var item in result.Data)
            {
                yield return item;
            }
        }
        while (count < total);
    }
}