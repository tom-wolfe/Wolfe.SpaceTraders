using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Agents;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersRegistrationExtensions
{
    public static RegisterResult ToDomain(
        this SpaceTradersRegistration response,
        IShipClient shipClient,
        IContractClient contractClient
    ) => new()
    {
        Agent = response.Agent.ToDomain(),
        Contract = response.Contract.ToDomain(contractClient),
        Faction = response.Faction.ToDomain(),
        Ship = response.Ship.ToDomain(shipClient),
        Token = response.Token
    };
}