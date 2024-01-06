using System.Diagnostics;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

/// <summary>
/// Represents an item that can be bought, sold or traded.
/// </summary>
[StronglyTypedId]
[DebuggerDisplay("{Value}")]
public readonly partial struct ItemId
{
    /// <summary>
    /// Fuel used to power ships.
    /// </summary>
    public static readonly ItemId Fuel = new("FUEL");
}