namespace Wolfe.SpaceTraders.Domain.Contracts;

public class ContractTerms
{
    /// <summary>
    /// The deadline for the contract.
    /// </summary>
    public required DateTimeOffset Deadline { get; init; }

    /// <summary>
    /// Payments for the contract.
    /// </summary>
    public required ContractPaymentTerms Payment { get; init; } = new();

    /// <summary>
    /// The cargo that needs to be delivered to fulfill the contract.
    /// </summary>
    public required IReadOnlyCollection<ContractItem> Items { get; init; }
}