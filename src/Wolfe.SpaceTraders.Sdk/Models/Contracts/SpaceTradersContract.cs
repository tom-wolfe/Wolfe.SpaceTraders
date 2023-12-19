namespace Wolfe.SpaceTraders.Sdk.Models.Contracts;

public class SpaceTradersContract
{
    public required string Id { get; set; }
    public required string FactionSymbol { get; set; }
    public required string Type { get; set; }
    public required SpaceTradersContractTerms Terms { get; set; }
    public bool Fulfilled { get; set; }
    public bool Accepted { get; set; }
    public DateTimeOffset DeadlineToAccept { get; set; }
}