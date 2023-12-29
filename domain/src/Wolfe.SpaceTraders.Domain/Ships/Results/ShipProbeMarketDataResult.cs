using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain.Ships.Results;

public class ShipProbeMarketDataResult
{
    public required MarketData? Data { get; init; }
}