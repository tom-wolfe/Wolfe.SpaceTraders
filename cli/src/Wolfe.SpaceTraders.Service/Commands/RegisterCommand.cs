using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Agents;

namespace Wolfe.SpaceTraders.Service.Commands;

public class RegisterCommand
{
    public required FactionSymbol Faction { get; set; }
    public required AgentSymbol Symbol { get; set; }
    public string? Email { get; set; }
}