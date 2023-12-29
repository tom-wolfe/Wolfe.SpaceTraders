using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;

namespace Wolfe.SpaceTraders.Domain.Ships;

public interface IShipClient
{
    public Task<ShipDockResult> Dock(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipExtractResult> Extract(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipNavigation> GetNavigation(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipJettisonResult> Jettison(ShipId shipId, ShipJettisonCommand command, CancellationToken cancellationToken = default);
    public Task<ShipNavigateResult> Navigate(ShipId shipId, ShipNavigateCommand command, CancellationToken cancellationToken = default);
    public Task<ShipOrbitResult> Orbit(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipProbeMarketDataResult?> ProbeMarketData(WaypointId waypointId, CancellationToken cancellationToken = default);
    public Task<ShipRefuelResult> Refuel(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipSellResult> Sell(ShipId shipId, ShipSellCommand request, CancellationToken cancellationToken = default);
    public Task<SetShipSpeedResult> SetSpeed(ShipId shipId, ShipSpeed speed, CancellationToken cancellationToken = default);
}