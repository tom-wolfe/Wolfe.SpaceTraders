using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersContractTermsExtensions
{
    public static ContractTerms ToDomain(this SpaceTradersContractTerms terms) => new()
    {
        Deadline = terms.Deadline,
        Payment = terms.Payment.ToDomain(),
        Items = terms.Deliver.Select(d => d.ToDomain()).ToList(),
    };
}