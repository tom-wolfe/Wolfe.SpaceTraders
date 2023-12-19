using Wolfe.SpaceTraders.Sdk.Models;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersErrorResponse
{
    public SpaceTradersError Error { get; set; } = new();
}