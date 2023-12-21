namespace Wolfe.SpaceTraders.Domain.Models.Contracts;

public class ContractTerms
{
    public required DateTimeOffset Deadline { get; set; }
    public required ContractPaymentTerms Payment { get; set; } = new();
    public required IReadOnlyCollection<ContractGood> Deliver { get; set; }
}