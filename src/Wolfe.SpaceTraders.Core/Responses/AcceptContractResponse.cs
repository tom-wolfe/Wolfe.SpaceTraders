using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Core.Responses;

public class AcceptContractResponse
{
    public required Agent Agent { get; set; }
    public required Contract Contract { get; set; }
}