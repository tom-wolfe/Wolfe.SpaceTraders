using System.Net;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Agents;
using Wolfe.SpaceTraders.Domain.Models.Contracts;
using Wolfe.SpaceTraders.Domain.Models.Ships;
using Wolfe.SpaceTraders.Domain.Models.Shipyards;
using Wolfe.SpaceTraders.Domain.Models.Systems;
using Wolfe.SpaceTraders.Domain.Models.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Extensions;
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

internal class SpaceTradersClient(ISpaceTradersApiClient client) : ISpaceTradersClient
{
    public async Task<RegisterResult> Register(RegisterCommand command, CancellationToken cancellationToken = default)
    {
        var response = await client.Register(command.ToApi(), cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<Agent> GetAgent(CancellationToken cancellationToken = default)
    {
        var response = await client.GetAgent(cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default)
    {
        return AsyncEnumerate<SpaceTradersContract>(
            async p => (await client.GetContracts(20, p, cancellationToken)).GetContent()
        ).SelectAwait(c => ValueTask.FromResult(c.ToDomain()));
    }

    public async Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default)
    {
        var response = await client.GetContract(contractId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain();
    }

    public async Task<AcceptContractResult> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default)
    {
        var response = await client.AcceptContract(contractId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<Shipyard?> GetShipyard(WaypointSymbol waypointId, CancellationToken cancellationToken = default)
    {
        var response = await client.GetShipyard(waypointId.System.Value, waypointId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain();
    }

    public async Task<PurchaseShipResult> PurchaseShip(PurchaseShipCommand command, CancellationToken cancellationToken = default)
    {
        var response = await client.PurchaseShip(command.ToApi(), cancellationToken);
        return response.GetContent().ToDomain();
    }

    public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default)
    {
        return AsyncEnumerate<SpaceTradersShip>(
            async p => (await client.GetShips(20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));
    }

    public async Task<Ship?> GetShip(ShipSymbol shipId, CancellationToken cancellationToken = default)
    {
        var response = await client.GetShip(shipId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain();
    }

    public async Task<SetShipSpeedResult> SetShipSpeed(ShipSymbol shipId, FlightSpeed speed, CancellationToken cancellationToken = default)
    {
        var request = new SpaceTradersPatchShipNavRequest { FlightMode = speed.Value };
        var response = await client.PatchShipNav(shipId.Value, request, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipOrbitResult> ShipOrbit(ShipSymbol shipId, CancellationToken cancellationToken = default)
    {
        var response = await client.ShipOrbit(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipDockResult> ShipDock(ShipSymbol shipId, CancellationToken cancellationToken = default)
    {
        var response = await client.ShipDock(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipNavigateResult> ShipNavigate(ShipSymbol shipId, ShipNavigateCommand command, CancellationToken cancellationToken = default)
    {
        var response = await client.ShipNavigate(shipId.Value, command.ToApi(), cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipRefuelResult> ShipRefuel(ShipSymbol shipId, CancellationToken cancellationToken = default)
    {
        var response = await client.ShipRefuel(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipSellResult> ShipSell(ShipSymbol shipId, ShipSellCommand command, CancellationToken cancellationToken = default)
    {
        var request = new SpaceTradersShipSellRequest
        {
            Symbol = command.ItemId.Value,
            Units = command.Quantity
        };
        var response = await client.ShipSell(shipId.Value, request, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipExtractResult> ShipExtract(ShipSymbol shipId, CancellationToken cancellationToken)
    {
        var response = await client.ShipExtract(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public IAsyncEnumerable<StarSystem> GetSystems(CancellationToken cancellationToken = default)
    {
        return AsyncEnumerate<SpaceTradersSystem>(
            async p => (await client.GetSystems(20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));
    }

    public async Task<StarSystem?> GetSystem(SystemSymbol systemId, CancellationToken cancellationToken = default)
    {
        var response = await client.GetSystem(systemId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain();
    }

    public IAsyncEnumerable<Waypoint> GetWaypoints(SystemSymbol systemId, WaypointType? type, IEnumerable<WaypointTraitSymbol> traits, CancellationToken cancellationToken = default)
    {
        var typeString = type?.Value;
        var traitStrings = traits.Select(t => t.Value).ToArray();
        return AsyncEnumerate<SpaceTradersWaypoint>(
            async p => (await client.GetWaypoints(systemId.Value, typeString, traitStrings, 20, p, cancellationToken)).GetContent()
        ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));
    }

    public async Task<Waypoint?> GetWaypoint(WaypointSymbol waypointId, CancellationToken cancellationToken = default)
    {
        var response = await client.GetWaypoint(waypointId.System.Value, waypointId.Value, cancellationToken);
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