using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersContractExtensions
{
    public static Contract ToDomain(this SpaceTradersContract contract) => new()
    {
        Id = new ContractId(contract.Id),
        FactionId = new FactionId(contract.FactionSymbol),
        Type = new ContractType(contract.Type),
        Terms = contract.Terms.ToDomain(),
        Fulfilled = contract.Fulfilled,
        Accepted = contract.Accepted,
        DeadlineToAccept = contract.DeadlineToAccept,
    };
}