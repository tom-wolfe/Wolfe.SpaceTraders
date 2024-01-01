using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Factions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Agents.Results;

public class CreateAgentResult
{
    public required Agent Agent { get; init; }
    public required Contract Contract { get; init; }
    public required Faction Faction { get; init; }
    public required Ship Ship { get; init; }
    public required string Token { get; init; }
}