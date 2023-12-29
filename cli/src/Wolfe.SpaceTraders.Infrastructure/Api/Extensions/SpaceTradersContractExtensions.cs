using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Factions;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersContractExtensions
{
    public static Contract ToDomain(this SpaceTradersContract contract, IContractClient client) => new(client, contract.Accepted)
    {
        Id = new ContractId(contract.Id),
        FactionId = new FactionId(contract.FactionSymbol),
        Type = new ContractType(contract.Type),
        Terms = contract.Terms.ToDomain(),
        Fulfilled = contract.Fulfilled,
        DeadlineToAccept = contract.DeadlineToAccept,
    };
}