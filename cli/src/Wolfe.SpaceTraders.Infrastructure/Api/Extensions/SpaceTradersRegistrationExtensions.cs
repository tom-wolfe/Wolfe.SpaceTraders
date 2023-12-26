using Wolfe.SpaceTraders.Sdk.Models.Agents;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersRegistrationExtensions
{
    public static RegisterResult ToDomain(this SpaceTradersRegistration response) => new()
    {
        Agent = response.Agent.ToDomain(),
        Contract = response.Contract.ToDomain(),
        Faction = response.Faction.ToDomain(),
        Ship = response.Ship.ToDomain(),
        Token = response.Token
    };
}