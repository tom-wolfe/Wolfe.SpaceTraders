namespace Wolfe.SpaceTraders.Models;

public class ContractTerms
{
    public DateTimeOffset Deadline { get; set; }
    public ContractPaymentTerms Payment { get; set; } = new();
    public required List<ContractDeliverGood> Deliver { get; set; }
}