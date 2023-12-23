using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Contracts;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersContractPaymentTermsExtensions
{
    public static ContractPaymentTerms ToDomain(this SpaceTradersContractPaymentTerms terms) => new()
    {
        OnAccepted = new Credits(terms.OnAccepted),
        OnFulfilled = new Credits(terms.OnFulfilled),
    };
}