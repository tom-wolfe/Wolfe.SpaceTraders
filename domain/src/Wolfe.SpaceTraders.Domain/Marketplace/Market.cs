using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

public class Market
{
    public required WaypointSymbol Symbol { get; set; }
    public required List<MarketItem> Imports { get; set; } = [];
    public required List<MarketItem> Exports { get; set; } = [];
    public required List<MarketItem> Exchange { get; set; } = [];
}