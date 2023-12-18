using Wolfe.SpaceTraders.Core.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Requests;

internal class ApiRegisterRequest
{
    public required string Faction { get; set; }
    public required string Symbol { get; set; }
    public string? Email { get; set; }

    public static ApiRegisterRequest FromDomain(RegisterRequest request) => new()
    {
        Faction = request.Faction.Value,
        Symbol = request.Symbol.Value,
        Email = request.Email
    };
}