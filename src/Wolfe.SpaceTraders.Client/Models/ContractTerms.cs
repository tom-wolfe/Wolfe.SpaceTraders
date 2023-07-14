namespace Wolfe.SpaceTraders.Models;

public class ContractTerms
{
    public DateTimeOffset Deadline { get; set; }
    public ContractPaymentTerms Payment { get; set; } = new();
    public List<ContractDeliverGood> Deliver { get; set; } = new();
}