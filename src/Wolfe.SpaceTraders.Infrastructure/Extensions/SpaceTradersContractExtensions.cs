using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersContractExtensions
{
    public static Contract ToDomain(this SpaceTradersContract contract) => new()
    {
        Id = new ContractId(contract.Id),
        FactionSymbol = new FactionSymbol(contract.FactionSymbol),
        Type = new ContractType(contract.Type),
        Terms = contract.Terms.ToDomain(),
        Fulfilled = contract.Fulfilled,
        Accepted = contract.Accepted,
        DeadlineToAccept = contract.DeadlineToAccept,
    };
}