using Wolfe.SpaceTraders.Models;
using Wolfe.SpaceTraders.Requests;
using Wolfe.SpaceTraders.Responses;

namespace Wolfe.SpaceTraders;

public interface ISpaceTradersClient
{
    public Task<Agent> GetAgent(CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default);
    public Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default);
    public Task<AcceptContractResponse> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<ShallowStarSystem> GetSystems(CancellationToken cancellationToken = default);
    public Task<StarSystem?> GetSystem(SystemSymbol systemId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<ShallowWaypoint> GetWaypoints(SystemSymbol systemId, CancellationToken cancellationToken = default);
    public Task<Waypoint?> GetWaypoint(WaypointSymbol waypointId, CancellationToken cancellationToken = default);
    public Task<Shipyard?> GetShipyard(WaypointSymbol waypointId, CancellationToken cancellationToken = default);
    public Task<PurchaseShipResponse> PurchaseShip(PurchaseShipRequest request, CancellationToken cancellationToken = default);
}