namespace Wolfe.SpaceTraders.Domain.Models.Ships;

public class ShipCargoItem
{
    public required TradeSymbol Symbol { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Units { get; set; }
}