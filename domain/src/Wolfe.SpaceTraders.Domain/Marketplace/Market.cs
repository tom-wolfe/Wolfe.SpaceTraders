using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

public class Market : Waypoint
{
    public required List<MarketItem> Imports { get; init; } = [];
    public required List<MarketItem> Exports { get; init; } = [];
    public required List<MarketItem> Exchange { get; init; } = [];
}