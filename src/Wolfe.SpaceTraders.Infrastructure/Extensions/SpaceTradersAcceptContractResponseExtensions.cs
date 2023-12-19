using Wolfe.SpaceTraders.Sdk.Models.Contracts;
using Wolfe.SpaceTraders.Service.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersAcceptContractResponseExtensions
{
    public static AcceptContractResponse ToDomain(this SpaceTradersAcceptedContract contract) => new()
    {
        Agent = contract.Agent.ToDomain(),
        Contract = contract.Contract.ToDomain(),
    };
}