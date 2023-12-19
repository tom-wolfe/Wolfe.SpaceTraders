using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Service.Requests;

public class RegisterRequest
{
    public required FactionSymbol Faction { get; set; }
    public required AgentSymbol Symbol { get; set; }
    public string? Email { get; set; }
}