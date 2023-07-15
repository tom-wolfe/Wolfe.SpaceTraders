using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Core.Requests;

public class RegisterRequest
{
    public required FactionSymbol Faction { get; set; } = FactionSymbol.Cosmic;
    public required AgentSymbol Symbol { get; set; }
    public string? Email { get; set; }
}