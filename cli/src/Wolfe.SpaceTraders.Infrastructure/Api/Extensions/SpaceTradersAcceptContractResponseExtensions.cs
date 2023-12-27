using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Contracts.Results;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersAcceptContractResponseExtensions
{
    public static AcceptContractResult ToDomain(this SpaceTradersAcceptedContract contract, IContractClient client) => new()
    {
        Agent = contract.Agent.ToDomain(),
        Contract = contract.Contract.ToDomain(client),
    };
}