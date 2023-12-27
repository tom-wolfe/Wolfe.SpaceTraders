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
    public Task<Marketplace?> GetMarketplace(WaypointId waypointId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Marketplace> GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default);
    public Task<Ship?> GetShip(ShipId shipId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default);
    public Task<Shipyard?> GetShipyard(WaypointId waypointId, CancellationToken cancellationToken = default);
    public Task<StarSystem?> GetSystem(SystemId systemId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<StarSystem> GetSystems(CancellationToken cancellationToken = default);
    public Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Waypoint> GetWaypoints(SystemId systemId, CancellationToken cancellationToken = default);
    public Task<PurchaseShipResult> PurchaseShip(PurchaseShipCommand command, CancellationToken cancellationToken = default);
    public Task<RegisterResult> Register(RegisterCommand command, CancellationToken cancellationToken = default);
    public Task<ShipDockResult> ShipDock(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipExtractResult> ShipExtract(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipNavigateResult> ShipNavigate(ShipId shipId, ShipNavigateCommand command, CancellationToken cancellationToken = default);
    public Task<ShipOrbitResult> ShipOrbit(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipRefuelResult> ShipRefuel(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipSellResult> ShipSell(ShipId shipId, ShipSellCommand request, CancellationToken cancellationToken = default);
    public Task<SetShipSpeedResult> SetShipSpeed(ShipId shipId, FlightSpeed speed, CancellationToken cancellationToken = default);
}