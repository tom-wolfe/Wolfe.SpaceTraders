using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersAcceptContractResponseExtensions
{
    public static AcceptContractResult ToDomain(this SpaceTradersAcceptedContract contract, IContractClient client) => new()
    {
        Agent = contract.Agent.ToDomain(),
        Contract = contract.Contract.ToDomain(client),
    };
}