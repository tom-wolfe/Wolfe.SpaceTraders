namespace Wolfe.SpaceTraders.Domain.Models;

public class Contract
{
    public required ContractId Id { get; set; }
    public required FactionSymbol FactionSymbol { get; set; }
    public required ContractType Type { get; set; }
    public required ContractTerms Terms { get; set; }
    public bool Fulfilled { get; set; }
    public bool Accepted { get; set; }
    public DateTimeOffset DeadlineToAccept { get; set; }
}