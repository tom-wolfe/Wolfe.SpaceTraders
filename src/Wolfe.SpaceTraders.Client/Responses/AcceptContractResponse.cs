using Wolfe.SpaceTraders.Models;

namespace Wolfe.SpaceTraders.Responses;

public class AcceptContractResponse
{
    public required Agent Agent { get; set; }
    public required Contract Contract { get; set; }
}