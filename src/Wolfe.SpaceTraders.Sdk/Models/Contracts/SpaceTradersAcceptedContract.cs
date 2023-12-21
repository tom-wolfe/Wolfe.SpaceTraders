using Wolfe.SpaceTraders.Sdk.Models.Agents;

namespace Wolfe.SpaceTraders.Sdk.Models.Contracts;

public class SpaceTradersAcceptedContract
{
    public required SpaceTradersAgent Agent { get; set; }
    public required SpaceTradersContract Contract { get; set; }
}