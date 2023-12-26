using System.Diagnostics;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

[DebuggerDisplay("{Name} ({Symbol})")]
public class MarketplaceItem
{
    public required TradeSymbol Symbol { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}