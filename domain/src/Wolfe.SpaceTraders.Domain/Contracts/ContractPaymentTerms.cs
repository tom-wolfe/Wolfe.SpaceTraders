namespace Wolfe.SpaceTraders.Domain.Contracts;

public class ContractPaymentTerms
{
    public Credits OnAccepted { get; init; } = Credits.Zero;
    public Credits OnFulfilled { get; init; } = Credits.Zero;
}