using System.Diagnostics;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

[DebuggerDisplay("{Name} ({Symbol})")]
public class MarketItem
{
    public required TradeSymbol Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}