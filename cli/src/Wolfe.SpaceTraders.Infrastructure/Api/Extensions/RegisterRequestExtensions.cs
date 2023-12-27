using Wolfe.SpaceTraders.Sdk.Requests;
using Wolfe.SpaceTraders.Service.Commands;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class RegisterRequestExtensions
{
    public static SpaceTradersRegisterRequest ToApi(this RegisterCommand command) => new()
    {
        Symbol = command.Agent.Value,
        Faction = command.Faction.Value,
        Email = command.Email
    };
}