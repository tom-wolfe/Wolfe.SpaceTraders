using Refit;
using System.Net;
using Wolfe.SpaceTraders.Models;
using Wolfe.SpaceTraders.Requests;
using Wolfe.SpaceTraders.Responses;

namespace Wolfe.SpaceTraders
{
    internal class SpaceTradersClient : ISpaceTradersClient
    {
        private readonly ISpaceTradersApiClient _client;

        public SpaceTradersClient(ISpaceTradersApiClient client)
        {
            _client = client;
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

        public IAsyncEnumerable<ShallowShip> GetShips(CancellationToken cancellationToken = default)
        {
            return AsyncEnumerate(10, (limit, page) => _client.GetShips(limit, page, cancellationToken));
        }

        public async Task<Ship?> GetShip(ShipSymbol shipId, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetShip(shipId, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
            response.EnsureSuccessStatusCode();
            return response.Content!.Data;
        }


        public IAsyncEnumerable<ShallowStarSystem> GetSystems(CancellationToken cancellationToken = default)
        {
            return AsyncEnumerate(10, (limit, page) => _client.GetSystems(limit, page, cancellationToken));
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
