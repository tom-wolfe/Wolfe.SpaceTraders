using Refit;
using System.Net;
using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Core.Requests;
using Wolfe.SpaceTraders.Core.Responses;
using Wolfe.SpaceTraders.Infrastructure.Extensions;
using Wolfe.SpaceTraders.Service;

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
            var response = await _client.Register(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public async Task<Agent> GetAgent(CancellationToken cancellationToken = default)
        {
            var response = await _client.GetAgent(cancellationToken);
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default)
        {
            return AsyncEnumerate(10, (limit, page) => _client.GetContracts(limit, page, cancellationToken));
        }

        public async Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetContract(contractId, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public async Task<AcceptContractResponse> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default)
        {
            var response = await _client.AcceptContract(contractId, cancellationToken);
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public async Task<Shipyard?> GetShipyard(WaypointSymbol waypointId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetShipyard(waypointId.SystemSymbol, waypointId, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public async Task<PurchaseShipResponse> PurchaseShip(PurchaseShipRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _client.PurchaseShip(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default)
        {
            return AsyncEnumerate(50, (limit, page) => _client.GetShips(limit, page, cancellationToken));
        }

        public async Task<Ship?> GetShip(ShipSymbol shipId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetShip(shipId, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public async Task<ShipOrbitResponse> ShipOrbit(ShipSymbol shipId, CancellationToken cancellationToken = default)
        {
            var response = await _client.ShipOrbit(shipId, cancellationToken);
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public async Task<ShipDockResponse> ShipDock(ShipSymbol shipId, CancellationToken cancellationToken = default)
        {
            var response = await _client.ShipDock(shipId, cancellationToken);
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public async Task<ShipNavigateResponse> ShipNavigate(ShipSymbol shipId, ShipNavigateRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _client.ShipNavigate(shipId, request, cancellationToken);
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public async Task<ShipRefuelResponse> ShipRefuel(ShipSymbol shipId, CancellationToken cancellationToken = default)
        {
            var response = await _client.ShipRefuel(shipId, cancellationToken);
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public async Task<GetSystemsResponse> GetSystems(int limit, int page, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetSystems(limit, page, cancellationToken);
            response.EnsureSuccessStatusCode();
            var meta = response.Content!.Meta;
            var remaining = meta.Total - meta.Page * meta.Limit;
            return new GetSystemsResponse
            {
                Systems = response.Content!.Data.ToList(),
                Remaining = remaining
            };
        }

        public async Task<StarSystem?> GetSystem(SystemSymbol systemId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetSystem(systemId, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        public IAsyncEnumerable<ShallowWaypoint> GetWaypoints(SystemSymbol systemId, CancellationToken cancellationToken = default)
        {
            return AsyncEnumerate(10, (limit, page) => _client.GetWaypoints(systemId, limit, page, cancellationToken));
        }

        public async Task<Waypoint?> GetWaypoint(WaypointSymbol waypointId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetWaypoint(waypointId.SystemSymbol, waypointId, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }

        private static async IAsyncEnumerable<T> AsyncEnumerate<T>(int limit, Func<int, int, Task<IApiResponse<SpaceTradersListResponse<T>>>> enumerate)
        {
            var page = 1;
            var count = 0;
            int total;

            do
            {
                var response = await enumerate(limit, page);
                response.EnsureSuccessStatusCode();

                var content = response.Content!;
                count += content.Meta.Limit;
                total = content.Meta.Total;
                page = content.Meta.Page + 1;

                foreach (var x in content.Data)
                    yield return x!;
            } while (count < total);
        }
    }
}
