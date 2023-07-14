namespace Wolfe.SpaceTraders.Models;

public class Contract
{
    public required string Id { get; set; }
    public required FactionSymbol FactionSymbol { get; set; }
    public required ContractType Type { get; set; }
    public ContractTerms Terms { get; set; } = new();
    public bool Fulfilled { get; set; }
    public bool Accepted { get; set; }
    public DateTimeOffset DeadlineToAccept { get; set; }
}