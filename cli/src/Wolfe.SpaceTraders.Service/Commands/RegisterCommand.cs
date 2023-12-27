using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Agents;

namespace Wolfe.SpaceTraders.Service.Commands;

public class RegisterCommand
{
    public required FactionId Faction { get; init; }
    public required AgentId Agent { get; init; }
    public string? Email { get; init; }
}