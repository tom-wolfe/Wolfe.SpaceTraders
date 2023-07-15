namespace Wolfe.SpaceTraders.Core.Models;

public class ContractDeliverGood
{
    public required TradeSymbol TradeSymbol { get; set; }
    public required string DestinationSymbol { get; set; }
    public int UnitsRequired { get; set; }
    public int UnitsFulfilled { get; set; }
}