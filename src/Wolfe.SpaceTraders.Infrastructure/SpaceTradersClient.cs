using System.Net;
using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Infrastructure.Extensions;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Models;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;
using Wolfe.SpaceTraders.Sdk.Models.Ships;
using Wolfe.SpaceTraders.Sdk.Responses;
using Wolfe.SpaceTraders.Service;
using Wolfe.SpaceTraders.Service.Requests;
using Wolfe.SpaceTraders.Service.Responses;

namespace Wolfe.SpaceTraders.Infrastructure
{
    internal class SpaceTradersClient : ISpaceTradersClient
    {
        private readonly ISpaceTradersApiClient _client;

        public SpaceTradersClient(ISpaceTradersApiClient client)
        {
            _client = client;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _client.Register(request.ToApi(), cancellationToken);
            return response.GetContent().Data.ToDomain();
        }

        public async Task<Agent> GetAgent(CancellationToken cancellationToken = default)
        {
            var response = await _client.GetAgent(cancellationToken);
            return response.GetContent().Data.ToDomain();
        }

        public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default)
        {
            return AsyncEnumerate<SpaceTradersContract>(
                async p => (await _client.GetContracts(20, p, cancellationToken)).GetContent()
            ).SelectAwait(c => ValueTask.FromResult(c.ToDomain()));
        }

        public async Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetContract(contractId.Value, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            return response.GetContent().Data.ToDomain();
        }

        public async Task<AcceptContractResponse> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default)
        {
            var response = await _client.AcceptContract(contractId.Value, cancellationToken);
            return response.GetContent().Data.ToDomain();
        }

        public async Task<Shipyard?> GetShipyard(WaypointSymbol waypointId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetShipyard(waypointId.System.Value, waypointId.Value, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            return response.GetContent().Data.ToDomain();
        }

        public async Task<PurchaseShipResponse> PurchaseShip(PurchaseShipRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _client.PurchaseShip(request.ToApi(), cancellationToken);
            return response.GetContent().ToDomain();
        }

        public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default)
        {
            return AsyncEnumerate<SpaceTradersShip>(
                async p => (await _client.GetShips(20, p, cancellationToken)).GetContent()
            ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));
        }

        public async Task<Ship?> GetShip(ShipSymbol shipId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetShip(shipId.Value, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            return response.GetContent().Data.ToDomain();
        }

        public async Task<ShipOrbitResponse> ShipOrbit(ShipSymbol shipId, CancellationToken cancellationToken = default)
        {
            var response = await _client.ShipOrbit(shipId.Value, cancellationToken);
            return response.GetContent().ToDomain();
        }

        public async Task<ShipDockResponse> ShipDock(ShipSymbol shipId, CancellationToken cancellationToken = default)
        {
            var response = await _client.ShipDock(shipId.Value, cancellationToken);
            return response.GetContent().ToDomain();
        }

        public async Task<ShipNavigateResponse> ShipNavigate(ShipSymbol shipId, ShipNavigateRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _client.ShipNavigate(shipId.Value, request.ToApi(), cancellationToken);
            return response.GetContent().ToDomain();
        }

        public async Task<ShipRefuelResponse> ShipRefuel(ShipSymbol shipId, CancellationToken cancellationToken = default)
        {
            var response = await _client.ShipRefuel(shipId.Value, cancellationToken);
            return response.GetContent().ToDomain();
        }

        public IAsyncEnumerable<StarSystem> GetSystems(CancellationToken cancellationToken = default)
        {
            return AsyncEnumerate<SpaceTradersSystem>(
                async p => (await _client.GetSystems(20, p, cancellationToken)).GetContent()
            ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));
        }

        public async Task<StarSystem?> GetSystem(SystemSymbol systemId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetSystem(systemId.Value, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            return response.GetContent().Data.ToDomain();
        }

        public IAsyncEnumerable<Waypoint> GetWaypoints(SystemSymbol systemId, WaypointType? type, IEnumerable<WaypointTraitSymbol> traits, CancellationToken cancellationToken = default)
        {
            var typeString = type?.Value;
            var traitStrings = traits.Select(t => t.Value).ToArray();
            return AsyncEnumerate<SpaceTradersWaypoint>(
                async p => (await _client.GetWaypoints(systemId.Value, typeString, traitStrings, 20, p, cancellationToken)).GetContent()
            ).SelectAwait(s => ValueTask.FromResult(s.ToDomain()));
        }

        public async Task<Waypoint?> GetWaypoint(WaypointSymbol waypointId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetWaypoint(waypointId.System.Value, waypointId.Value, cancellationToken);
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
}
