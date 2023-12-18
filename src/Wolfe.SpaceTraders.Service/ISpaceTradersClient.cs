using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Core.Requests;
using Wolfe.SpaceTraders.Core.Responses;

namespace Wolfe.SpaceTraders.Service;

public interface ISpaceTradersClient
{
    public Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cancellationToken = default);
    public Task<Agent> GetAgent(CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default);
    public Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default);
    public Task<AcceptContractResponse> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default);
    public Task<GetSystemsResponse> GetSystems(int limit, int page, CancellationToken cancellationToken = default);
    public Task<StarSystem?> GetSystem(SystemSymbol systemId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<ShallowWaypoint> GetWaypoints(SystemSymbol systemId, CancellationToken cancellationToken = default);
    public Task<Waypoint?> GetWaypoint(WaypointSymbol waypointId, CancellationToken cancellationToken = default);
    public Task<Shipyard?> GetShipyard(WaypointSymbol waypointId, CancellationToken cancellationToken = default);
    public Task<PurchaseShipResponse> PurchaseShip(PurchaseShipRequest request, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default);
    public Task<Ship?> GetShip(ShipSymbol shipId, CancellationToken cancellationToken = default);
    public Task<ShipOrbitResponse> ShipOrbit(ShipSymbol shipId, CancellationToken cancellationToken = default);
    public Task<ShipDockResponse> ShipDock(ShipSymbol shipId, CancellationToken cancellationToken = default);
    public Task<ShipNavigateResponse> ShipNavigate(ShipSymbol shipId, ShipNavigateRequest request, CancellationToken cancellationToken = default);
    public Task<ShipRefuelResponse> ShipRefuel(ShipSymbol shipId, CancellationToken cancellationToken);
}