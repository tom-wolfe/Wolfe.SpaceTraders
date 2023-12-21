namespace Wolfe.SpaceTraders.Domain.Models;

public class ContractGood
{
    public required TradeSymbol TradeSymbol { get; set; }
    public required WaypointSymbol DestinationSymbol { get; set; }
    public int UnitsRequired { get; set; }
    public int UnitsFulfilled { get; set; }
}