using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain.Contracts;

public class ContractItem
{
    public required ItemId ItemId { get; init; }
    public required WaypointId DestinationId { get; init; }
    public int QuantityRequired { get; init; }
    public int QuantityFulfilled { get; init; }
    public int QuantityRemaining => QuantityRequired - QuantityFulfilled;
}