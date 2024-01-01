using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain.Ships.Commands;

public class ShipJettisonCommand
{
    public required ItemId ItemId { get; init; }
    public required int Quantity { get; init; }
}