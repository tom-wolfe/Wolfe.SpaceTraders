using System.Diagnostics;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

[DebuggerDisplay("{Name} ({ItemId})")]
public class MarketplaceItem
{
    /// <summary>
    /// The good's ID.
    /// </summary>
    public required ItemId ItemId { get; init; }

    /// <summary>
    /// The name of the good.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The description of the good.
    /// </summary>
    public required string Description { get; init; }
}