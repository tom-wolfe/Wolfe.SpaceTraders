using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Results;

public class AcceptContractResult
{
    public required Agent Agent { get; set; }
    public required Contract Contract { get; set; }
}