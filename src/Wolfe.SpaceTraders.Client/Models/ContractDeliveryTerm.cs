namespace Wolfe.SpaceTraders.Models;

public class ContractDeliveryTerm
{
    public required string TradeSymbol { get; set; }
    public required string DestinationSymbol { get; set; }
    public int UnitsRequired { get; set; }
    public int UnitsFulfilled { get; set; }
}