using Wolfe.SpaceTraders.Sdk.Requests;
using Wolfe.SpaceTraders.Service.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class RegisterRequestExtensions
{
    public static SpaceTradersRegisterRequest ToApi(this RegisterRequest request) => new()
    {
        Symbol = request.Symbol.Value,
        Faction = request.Faction.Value,
        Email = request.Email
    };
}