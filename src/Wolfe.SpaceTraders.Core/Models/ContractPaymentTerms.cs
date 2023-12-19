namespace Wolfe.SpaceTraders.Core.Models;

public class ContractPaymentTerms
{
    public Credits OnAccepted { get; set; } = Credits.Zero;
    public Credits OnFulfilled { get; set; } = Credits.Zero;
}