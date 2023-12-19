using Wolfe.SpaceTraders.Sdk.Models;
using Wolfe.SpaceTraders.Service.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersRegistrationExtensions
{
    public static RegisterResponse ToDomain(this SpaceTradersRegistration response) => new()
    {
        Agent = response.Agent.ToDomain(),
        Contract = response.Contract.ToDomain(),
        Faction = response.Faction.ToDomain(),
        Ship = response.Ship.ToDomain(),
        Token = response.Token
    };
}