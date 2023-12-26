using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Service.Commands;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Service;

public interface ISpaceTradersClient
{
    public Task<AcceptContractResult> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default);
    public Task<Agent> GetAgent(CancellationToken cancellationToken = default);
    public Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default);
    public Task<Marketplace?> GetMarketplace(WaypointSymbol waypointId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Marketplace> GetMarketplaces(SystemSymbol systemId, CancellationToken cancellationToken = default);
    public Task<Ship?> GetShip(ShipSymbol shipId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default);
    public Task<Shipyard?> GetShipyard(WaypointSymbol waypointId, CancellationToken cancellationToken = default);
    public Task<StarSystem?> GetSystem(SystemSymbol systemId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<StarSystem> GetSystems(CancellationToken cancellationToken = default);
    public Task<Waypoint?> GetWaypoint(WaypointSymbol waypointId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Waypoint> GetWaypoints(SystemSymbol systemId, CancellationToken cancellationToken = default);
    public Task<PurchaseShipResult> PurchaseShip(PurchaseShipCommand command, CancellationToken cancellationToken = default);
    public Task<RegisterResult> Register(RegisterCommand command, CancellationToken cancellationToken = default);
    public Task<ShipDockResult> ShipDock(ShipSymbol shipId, CancellationToken cancellationToken = default);
    public Task<ShipExtractResult> ShipExtract(ShipSymbol shipId, CancellationToken cancellationToken = default);
    public Task<ShipNavigateResult> ShipNavigate(ShipSymbol shipId, ShipNavigateCommand command, CancellationToken cancellationToken = default);
    public Task<ShipOrbitResult> ShipOrbit(ShipSymbol shipId, CancellationToken cancellationToken = default);
    public Task<ShipRefuelResult> ShipRefuel(ShipSymbol shipId, CancellationToken cancellationToken = default);
    public Task<ShipSellResult> ShipSell(ShipSymbol shipId, ShipSellCommand request, CancellationToken cancellationToken = default);
    public Task<SetShipSpeedResult> SetShipSpeed(ShipSymbol shipId, FlightSpeed speed, CancellationToken cancellationToken = default);
}