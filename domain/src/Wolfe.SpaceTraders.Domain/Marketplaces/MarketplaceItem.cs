using System.Diagnostics;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

[DebuggerDisplay("{Name} ({ItemId})")]
public class MarketplaceItem
{
    public required ItemId ItemId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}