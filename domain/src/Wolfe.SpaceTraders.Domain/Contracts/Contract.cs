namespace Wolfe.SpaceTraders.Domain.Contracts;

public class Contract
{
    public required ContractId Id { get; init; }
    public required FactionSymbol FactionSymbol { get; init; }
    public required ContractType Type { get; init; }
    public required ContractTerms Terms { get; init; }
    public bool Fulfilled { get; init; }
    public bool Accepted { get; init; }
    public DateTimeOffset DeadlineToAccept { get; init; }
}