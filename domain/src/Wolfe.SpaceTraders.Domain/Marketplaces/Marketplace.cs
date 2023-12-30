using Wolfe.SpaceTraders.Domain.Exploration;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

public class Marketplace : Waypoint
{
    /// <summary>
    /// The list of goods that are sought as imports in this market.
    /// </summary>
    public required List<MarketplaceItem> Imports { get; init; } = [];

    /// <summary>
    /// The list of goods that are exported from this market.
    /// </summary>
    public required List<MarketplaceItem> Exports { get; init; } = [];

    /// <summary>
    /// The list of goods that are bought and sold between agents at this market.
    /// </summary>
    public required List<MarketplaceItem> Exchange { get; init; } = [];

    /// <summary>
    /// Checks whether the given item is bought in this market.
    /// </summary>
    /// <param name="itemId">The item to check.</param>
    /// <returns>True if the item is bought; otherwise, false.</returns>
    public bool IsBuying(ItemId itemId) => Imports.Any(e => e.ItemId == itemId) || Exchange.Any(e => e.ItemId == itemId);

    /// <summary>
    /// Checks whether the given item is sold in this market.
    /// </summary>
    /// <param name="itemId">The item to check.</param>
    /// <returns>True if the item is sold; otherwise, false.</returns>
    public bool IsSelling(ItemId itemId) => Exports.Any(e => e.ItemId == itemId) || Exchange.Any(e => e.ItemId == itemId);
}