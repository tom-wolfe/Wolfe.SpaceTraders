namespace Wolfe.SpaceTraders.Sdk.Models.Contracts;

public class SpaceTradersContractTerms
{
    public DateTimeOffset Deadline { get; set; }
    public SpaceTradersContractPaymentTerms Payment { get; set; } = new();
    public required IReadOnlyCollection<SpaceTradersContractGood> Deliver { get; set; }
}