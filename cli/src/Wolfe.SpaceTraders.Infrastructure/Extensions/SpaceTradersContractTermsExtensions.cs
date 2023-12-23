using Wolfe.SpaceTraders.Domain.Models.Contracts;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersContractTermsExtensions
{
    public static ContractTerms ToDomain(this SpaceTradersContractTerms terms) => new()
    {
        Deadline = terms.Deadline,
        Payment = terms.Payment.ToDomain(),
        Deliver = terms.Deliver.Select(d => d.ToDomain()).ToList(),
    };
}