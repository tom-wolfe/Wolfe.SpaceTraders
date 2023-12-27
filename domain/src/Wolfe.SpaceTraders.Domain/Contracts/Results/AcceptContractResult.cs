using Wolfe.SpaceTraders.Domain.Agents;

namespace Wolfe.SpaceTraders.Domain.Contracts.Results;

public class AcceptContractResult
{
    public required Agent Agent { get; init; }
    public required Contract Contract { get; init; }
}