using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Service.Results;

public class RegisterResult
{
    public required Agent Agent { get; set; }
    public required Contract Contract { get; set; }
    public required Faction Faction { get; set; }
    public required Ship Ship { get; set; }
    public required string Token { get; set; }
}