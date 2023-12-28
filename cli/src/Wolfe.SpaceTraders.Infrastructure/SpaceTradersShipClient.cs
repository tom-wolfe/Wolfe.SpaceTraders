using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Infrastructure.Api.Extensions;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Requests;

namespace Wolfe.SpaceTraders.Infrastructure;

internal class SpaceTradersShipClient(ISpaceTradersApiClient apiClient) : IShipClient
{
    public async Task<SetShipSpeedResult> SetSpeed(ShipId shipId, FlightSpeed speed, CancellationToken cancellationToken = default)
    {
        var request = new SpaceTradersPatchShipNavRequest { FlightMode = speed.Value };
        var response = await apiClient.PatchShipNav(shipId.Value, request, cancellationToken);
        return new SetShipSpeedResult
        {
            Navigation = response.GetContent().Data.ToDomain()
        };
    }

    public async Task<ShipDockResult> Dock(ShipId shipId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.ShipDock(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipExtractResult> Extract(ShipId shipId, CancellationToken cancellationToken)
    {
        var response = await apiClient.ShipExtract(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipJettisonResult> Jettison(ShipId shipId, ShipJettisonCommand command, CancellationToken cancellationToken)
    {
        var response = await apiClient.ShipJettison(shipId.Value, command.ToApi(), cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipNavigateResult> Navigate(ShipId shipId, ShipNavigateCommand command, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.ShipNavigate(shipId.Value, command.ToApi(), cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipOrbitResult> Orbit(ShipId shipId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.ShipOrbit(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipRefuelResult> Refuel(ShipId shipId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.ShipRefuel(shipId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<ShipSellResult> Sell(ShipId shipId, ShipSellCommand command, CancellationToken cancellationToken = default)
    {
        var request = new SpaceTradersShipSellRequest
        {
            Symbol = command.ItemId.Value,
            Quantity = command.Quantity
        };
        var response = await apiClient.ShipSell(shipId.Value, request, cancellationToken);
        return response.GetContent().Data.ToDomain();
    }
}