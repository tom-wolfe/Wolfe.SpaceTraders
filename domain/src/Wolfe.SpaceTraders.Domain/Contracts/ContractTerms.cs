namespace Wolfe.SpaceTraders.Domain.Contracts;

public class ContractTerms
{
    public required DateTimeOffset Deadline { get; init; }
    public required ContractPaymentTerms Payment { get; init; } = new();
    public required IReadOnlyCollection<ContractGood> Deliver { get; init; }
}