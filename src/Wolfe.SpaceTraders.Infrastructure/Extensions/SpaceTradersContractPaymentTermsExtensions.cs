using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersContractPaymentTermsExtensions
{
    public static ContractPaymentTerms ToDomain(this SpaceTradersContractPaymentTerms terms) => new()
    {
        OnAccepted = terms.OnAccepted,
        OnFulfilled = terms.OnFulfilled,
    };
}