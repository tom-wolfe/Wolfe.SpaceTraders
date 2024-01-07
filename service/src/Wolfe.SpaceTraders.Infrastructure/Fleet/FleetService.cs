using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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
    private readonly Subject<Ship> _shipRegistered = new();
    private readonly Dictionary<string, Ship> _ships = new();
    private readonly SemaphoreSlim _shipLock = new(1, 1);

    public IObservable<Ship> ShipRegistered => _shipRegistered.AsObservable();

    public async Task<PurchaseShipResult> PurchaseShip(PurchaseShipCommand command, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.PurchaseShip(command.ToApi(), cancellationToken);
        return response.GetContent().ToDomain(shipClient);
    }

    public async Task<Ship?> GetShip(ShipId shipId, CancellationToken cancellationToken = default)
    {
        await _shipLock.WaitAsync(cancellationToken);
        try
        {
            if (_ships.TryGetValue(shipId.Value, out var ship)) { return ship; }

            var me = (await apiClient.GetAgent(cancellationToken)).GetContent().Data;
            var response = await apiClient.GetShip(shipId.Value, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound) { return null; }

            var apiShip = response.GetContent().Data;
            ship = apiShip.ToDomain(new AgentId(me.Symbol), shipClient);
            _ships.Add(ship.Id.Value, ship);
            _shipRegistered.OnNext(ship);

            return ship;
        }
        finally
        {
            _shipLock.Release();
        }
    }

    public async IAsyncEnumerable<Ship> GetShips([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var me = (await apiClient.GetAgent(cancellationToken)).GetContent().Data;
        var results = PaginationHelpers.ToAsyncEnumerable<SpaceTradersShip>(async p => (await apiClient.GetShips(20, p, cancellationToken)).GetContent());
        await foreach (var result in results)
        {
            await _shipLock.WaitAsync(cancellationToken);
            try
            {
                if (!_ships.TryGetValue(result.Symbol, out var ship))
                {
                    ship = result.ToDomain(new AgentId(me.Symbol), shipClient);
                    _shipRegistered.OnNext(ship);
                }
                yield return ship;
            }
            finally
            {
                _shipLock.Release();
            }
        }
    }
}
