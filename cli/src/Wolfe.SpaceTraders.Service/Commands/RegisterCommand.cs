using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Agents;

namespace Wolfe.SpaceTraders.Service.Commands;

public class RegisterCommand
{
    public required FactionSymbol Faction { get; set; }
    public required AgentSymbol Symbol { get; set; }
    public string? Email { get; set; }
}