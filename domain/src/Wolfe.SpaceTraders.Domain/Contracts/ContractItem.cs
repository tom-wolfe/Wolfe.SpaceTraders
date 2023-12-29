using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain.Contracts;

public class ContractItem
{
    /// <summary>
    /// The ID of the item to deliver.
    /// </summary>
    public required ItemId ItemId { get; init; }

    /// <summary>
    /// The destination where goods need to be delivered.
    /// </summary>
    public required WaypointId DestinationId { get; init; }

    /// <summary>
    /// The number of units that need to be delivered on this contract.
    /// </summary>
    public int QuantityRequired { get; init; }

    /// <summary>
    /// The number of units fulfilled on this contract.
    /// </summary>
    public int QuantityFulfilled { get; init; }

    /// <summary>
    /// The number of units remaining to fulfill this contract.
    /// </summary>
    public int QuantityRemaining => QuantityRequired - QuantityFulfilled;
}