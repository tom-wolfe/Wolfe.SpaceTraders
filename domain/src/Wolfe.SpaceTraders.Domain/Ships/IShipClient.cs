using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;

namespace Wolfe.SpaceTraders.Domain.Ships;

public interface IShipClient
{
    public Task<Ship?> GetShip(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipDockResult> Dock(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipExtractResult> Extract(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipNavigateResult> Navigate(ShipId shipId, ShipNavigateCommand command, CancellationToken cancellationToken = default);
    public Task<ShipOrbitResult> Orbit(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipRefuelResult> Refuel(ShipId shipId, CancellationToken cancellationToken = default);
    public Task<ShipSellResult> Sell(ShipId shipId, ShipSellCommand request, CancellationToken cancellationToken = default);
    public Task<SetShipSpeedResult> SetSpeed(ShipId shipId, FlightSpeed speed, CancellationToken cancellationToken = default);
}