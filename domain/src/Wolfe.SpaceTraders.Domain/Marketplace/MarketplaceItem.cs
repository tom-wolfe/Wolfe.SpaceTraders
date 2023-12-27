using System.Diagnostics;

namespace Wolfe.SpaceTraders.Domain.Marketplace;

[DebuggerDisplay("{Name} ({TradeId})")]
public class MarketplaceItem
{
    public required TradeId TradeId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}