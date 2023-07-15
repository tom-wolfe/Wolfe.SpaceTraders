using Wolfe.SpaceTraders.Models;
using Wolfe.SpaceTraders.Responses;

namespace Wolfe.SpaceTraders;

public interface ISpaceTradersClient
{
    public Task<Agent> GetAgent(CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default);
    public Task<Contract?> GetContract(string contractId, CancellationToken cancellationToken = default);
    public Task<AcceptContractResponse> AcceptContract(string contractId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<ShallowStarSystem> GetSystems(CancellationToken cancellationToken = default);
    public Task<StarSystem?> GetSystem(string systemId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<ShallowWaypoint> GetWaypoints(string systemId, CancellationToken cancellationToken = default);
    public Task<Waypoint?> GetWaypoint(string waypointId, CancellationToken cancellationToken = default);
    public Task<Shipyard?> GetShipyard(string waypointId, CancellationToken cancellationToken = default);
}