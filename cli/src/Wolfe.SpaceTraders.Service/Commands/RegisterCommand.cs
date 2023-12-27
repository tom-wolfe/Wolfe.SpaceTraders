using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Agents;

namespace Wolfe.SpaceTraders.Service.Commands;

public class RegisterCommand
{
    public required FactionId Faction { get; set; }
    public required AgentId Agent { get; set; }
    public string? Email { get; set; }
}