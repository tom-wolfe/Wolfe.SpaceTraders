using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;

namespace Wolfe.SpaceTraders.Domain.Ships;

public interface IShipClient
{
    public Task Dock(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipExtractResult> Extract(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<IShipNavigation> GetNavigationStatus(ShipId shipId, CancellationToken cancellationToken = default);
    public Task Jettison(ShipId shipId, ShipJettisonCommand command, CancellationToken cancellationToken = default);
    public Task<ShipNavigateResult> Navigate(ShipId shipId, ShipNavigateCommand command, CancellationToken cancellationToken = default);
    public Task Orbit(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipProbeMarketDataResult?> ProbeMarketData(WaypointId waypointId, CancellationToken cancellationToken = default);
    public Task<ShipRefuelResult> Refuel(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipSellResult> Sell(ShipId shipId, ShipSellCommand request, CancellationToken cancellationToken = default);
    public Task SetSpeed(ShipId shipId, ShipSpeed speed, CancellationToken cancellationToken = default);
}